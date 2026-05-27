using HarmonyLib;

namespace KK_HCharaPosVR
{
    internal static class Hooks
    {
        internal static VRHScene? Scene;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(VRHScene), "MapSameObjectDisable")]
        internal static void MapSameObjectDisable(VRHScene __instance)
        {
            Scene = __instance;
            HCharaController.ResetScene();

            var heroines = __instance.flags.lstHeroine;
            for (int i = 0; i < heroines.Count; i++)
            {
                var type = i == 0 ? HCharaController.CharacterType.Female1
                                  : HCharaController.CharacterType.Female2;
                HCharaController.Get(heroines[i].chaCtrl)?.Init(type);
            }
            HCharaController.Get(__instance.flags.player.chaCtrl)?.Init(HCharaController.CharacterType.Male);
        }
    }
}
