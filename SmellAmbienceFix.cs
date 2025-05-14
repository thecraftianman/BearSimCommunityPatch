using HarmonyLib;
using UnityEngine;

namespace BearSimCommunityPatch;

[HarmonyPatch(typeof(FPSPlayer))]
internal class SmellAmbienceFix
{
    private static bool isSmellActuallyActive = false;

    [HarmonyPatch(typeof(HUDMain), "SmellActive")]
    [HarmonyPostfix]
    private static void SmellActive_Postfix()
    {
        isSmellActuallyActive = true;
    }

    [HarmonyPatch(typeof(HUDMain), "TabletActive")]
    [HarmonyPostfix]
    private static void TabletActive_Postfix()
    {
        isSmellActuallyActive = true;
    }

    [HarmonyPatch(typeof(FPSPlayer), "PauseOther")]
    [HarmonyPrefix]
    private static bool PauseOther_Prefix(ref string whatthing, FPSPlayer __instance, ref float ___pausedTime)
    {
        if (__instance is FPSPlayer player && !player.isKilled && Time.timeScale <= 0f)
        {
            // The game actually ends up passing a whatthing of "none" every time this function is used,
            // so let's make sure that the ambience isn't updated when we don't want it to be.
            // Spooky ambience is excluded from this because that one actually does need to be updated.
            if (whatthing != "smell" && !isSmellActuallyActive)
            {
                NightandDayController component = player.theSceneManager.GetComponent<NightandDayController>();
                component.UpdateAmbience();
            }

            Time.timeScale = ___pausedTime;
            HUDMenu.guiMenu.MenuNotActive();
            HUDMain.guiHud.SmellDisable();
            HUDMain.guiHud.SpookyDisable();
            HUDMain.guiHud.TabletUnactive();
            player.isPaused = false;
            player.smellActive = false;
            isSmellActuallyActive = false;

            return false;
        }

        return true;
    }
}