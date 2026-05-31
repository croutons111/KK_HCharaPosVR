using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using KKAPI.Chara;

namespace KK_HCharaPosVR
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid    = "KK_HCharaPosVR";
        public const string PluginName    = "KK_HCharaPosVR";
        public const string PluginVersion = "0.7.1";

        internal static new ManualLogSource Logger = null!;

        public static ConfigEntry<float> F1MoveScale = null!;
        public static ConfigEntry<float> F2MoveScale = null!;

        private readonly Harmony _harmony = new Harmony(PluginGuid);

        private void Awake()
        {
            Logger = base.Logger;

            F1MoveScale = Config.Bind("Female 1", "Move Scale", 1.0f, "Female1 の移動量の倍率");
            F2MoveScale = Config.Bind("Female 2", "Move Scale", 1.0f, "Female2 の移動量の倍率");

            _harmony.PatchAll(typeof(Hooks));
            CharacterApi.RegisterExtraBehaviour<HCharaController>(PluginGuid);
            Logger.LogInfo($"{PluginName} v{PluginVersion} loaded");
        }
    }
}
