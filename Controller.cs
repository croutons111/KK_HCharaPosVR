using System.Collections.Generic;
using System.Reflection;
using BepInEx.Configuration;
using HarmonyLib;
using KKAPI;
using KKAPI.Chara;
using UnityEngine;
using UnityEngine.VR;

namespace KK_HCharaPosVR
{
    public class HCharaController : CharaCustomFunctionController
    {
        public enum CharacterType { Female1, Female2, Male, Unknown }

        private CharacterType      _chaType  = CharacterType.Unknown;
        private ConfigEntry<float> _cfgScale = null!;

        // ---- 共有入力状態（static）----
        private static CharacterType _activeTarget  = CharacterType.Female1;
        private static bool          _female2Exists = false;

        private enum Phase { Idle, PressStarted, WaitSecondTap, Moving }
        private static Phase   _phase     = Phase.Idle;
        private static float   _phaseTime = 0f;
        private static bool    _prevPress = false;
        private static Vector3 _lastHandPos;
        private static Vector3 _moveDelta;
        private static int     _lastFrame = -1;

        // 長押し判定：200ms 以上でリピート移動モード、400ms 以内の2回タップで切り替え
        private const float HoldThreshold   = 0.20f;
        private const float DoubleTapWindow = 0.40f;

        // VRHScene → VRViveCameraManager → VRViveControllerManager → lstController → device
        private static readonly FieldInfo _fManagerVR = AccessTools.Field(typeof(VRHScene),                 "managerVR");
        private static readonly FieldInfo _fCtrlMgr   = AccessTools.Field(typeof(VRViveCameraManager),     "scrControllerManager");
        private static readonly FieldInfo _fLstCtrl   = AccessTools.Field(typeof(VRViveControllerManager), "lstController");
        private static readonly FieldInfo _fDevice    = AccessTools.Field(typeof(VRViveController),         "device");

        public static HCharaController? Get(ChaControl chaCtrl) =>
            chaCtrl.GetComponent<HCharaController>();

        internal static void ResetScene()
        {
            _female2Exists = false;
            _activeTarget  = CharacterType.Female1;
            _phase         = Phase.Idle;
            _prevPress     = false;
            _moveDelta     = Vector3.zero;
        }

        internal void Init(CharacterType chaType)
        {
            _chaType  = chaType;
            _cfgScale = chaType == CharacterType.Female2 ? Plugin.F2MoveScale : Plugin.F1MoveScale;
            if (chaType == CharacterType.Female2) _female2Exists = true;
        }

        protected override void OnCardBeingSaved(GameMode currentGameMode) { }

        protected override void Update()
        {
            if (!Plugin.IsEnabled ||
                !VRDevice.isPresent ||
                (_chaType != CharacterType.Female1 && _chaType != CharacterType.Female2))
            {
                base.Update();
                return;
            }

            // 入力処理は 1 フレームに 1 回だけ（Female1/2 どちらか先に実行した方が担当）
            if (Time.frameCount != _lastFrame)
            {
                ProcessInput();
                _lastFrame = Time.frameCount;
            }

            // アクティブ対象のみ移動を適用
            if (_phase == Phase.Moving && _chaType == _activeTarget)
            {
                ChaControl.transform.position +=
                    Camera.main.transform.rotation * (_moveDelta * _cfgScale.Value);
            }

            base.Update();
        }

        private static void ProcessInput()
        {
            _moveDelta = Vector3.zero;

            bool  pressed = IsAButtonPressed();
            float now     = Time.time;

            switch (_phase)
            {
                case Phase.Idle:
                    if (pressed && !_prevPress)
                    {
                        _phaseTime = now;
                        _phase     = Phase.PressStarted;
                    }
                    break;

                case Phase.PressStarted:
                    if (!pressed)
                    {
                        // 短く離した → 1回目タップ確定、2回目待ち
                        _phaseTime = now;
                        _phase     = Phase.WaitSecondTap;
                    }
                    else if (now - _phaseTime >= HoldThreshold)
                    {
                        // 長押し → 移動モード開始
                        _lastHandPos = GetRightHandPos();
                        _phase       = Phase.Moving;
                    }
                    break;

                case Phase.WaitSecondTap:
                    if (pressed && !_prevPress)
                    {
                        // 2回目タップ → Female1 ↔ Female2 切り替え
                        if (_female2Exists)
                        {
                            _activeTarget = _activeTarget == CharacterType.Female1
                                          ? CharacterType.Female2
                                          : CharacterType.Female1;
                            TriggerHaptic();
                        }
                        _phase = Phase.Idle;
                    }
                    else if (now - _phaseTime > DoubleTapWindow)
                    {
                        // タイムアウト → キャンセル（何もしない）
                        _phase = Phase.Idle;
                    }
                    break;

                case Phase.Moving:
                    var handPos = GetRightHandPos();
                    if (pressed)
                        _moveDelta = handPos - _lastHandPos;
                    else
                        _phase = Phase.Idle; // 離した → 位置確定
                    _lastHandPos = handPos;
                    break;
            }

            _prevPress = pressed;
        }

        // 右コントローラー固定・A ボタン固定（mask = 1<<7 = 0x80）
        private static bool IsAButtonPressed()
        {
            var scene = Hooks.Scene;
            if (scene == null) return false;

            var managerVR = _fManagerVR.GetValue(scene) as VRViveCameraManager;
            if (managerVR == null) return false;

            var ctrlMgr = _fCtrlMgr.GetValue(managerVR) as VRViveControllerManager;
            if (ctrlMgr == null) return false;

            var lstCtrl = _fLstCtrl.GetValue(ctrlMgr) as List<VRViveController>;
            if (lstCtrl == null || lstCtrl.Count < 2) return false;

            var dev = _fDevice.GetValue(lstCtrl[1]) as SteamVR_Controller.Device;
            return dev != null && dev.GetPress(0x80UL);
        }

        private static Vector3 GetRightHandPos() =>
            InputTracking.GetLocalPosition(VRNode.RightHand);

        private static void TriggerHaptic()
        {
            var scene = Hooks.Scene;
            if (scene == null) return;

            var managerVR = _fManagerVR.GetValue(scene) as VRViveCameraManager;
            if (managerVR == null) return;

            var ctrlMgr = _fCtrlMgr.GetValue(managerVR) as VRViveControllerManager;
            if (ctrlMgr == null) return;

            var lstCtrl = _fLstCtrl.GetValue(ctrlMgr) as List<VRViveController>;
            if (lstCtrl == null || lstCtrl.Count < 2) return;

            var dev = _fDevice.GetValue(lstCtrl[1]) as SteamVR_Controller.Device;
            dev?.TriggerHapticPulse(2000);
        }
    }
}
