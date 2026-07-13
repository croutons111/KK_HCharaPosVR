using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using KKAPI.Chara;

namespace KK_HCharaPosVR
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInProcess("KoikatuVR")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid    = "KK_HCharaPosVR";
        public const string PluginName    = "KK_HCharaPosVR";
        public const string PluginVersion = "0.8.0";

        internal static new ManualLogSource Logger = null!;

        public static ConfigEntry<bool>  MasterEnabled = null!;
        public static ConfigEntry<float> F1MoveScale   = null!;
        public static ConfigEntry<float> F2MoveScale   = null!;

        public static bool IsEnabled => MasterEnabled == null || MasterEnabled.Value;

        private readonly Harmony _harmony = new Harmony(PluginGuid);

        private void Awake()
        {
            Logger = base.Logger;

            MasterEnabled = Config.Bind("General", "Enabled", true,
                "Enable/disable all plugin features. OFF = vanilla behavior. When turned ON during an H scene, re-enter the H scene to take effect.");
            F1MoveScale = Config.Bind("Female 1", "Move Scale", 1.0f, "Movement scale for Female1");
            F2MoveScale = Config.Bind("Female 2", "Move Scale", 1.0f, "Movement scale for Female2");

            _harmony.PatchAll(typeof(Hooks));
            CharacterApi.RegisterExtraBehaviour<HCharaController>(PluginGuid);
            Logger.LogInfo($"{PluginName} v{PluginVersion} loaded");
        }
    }
}
