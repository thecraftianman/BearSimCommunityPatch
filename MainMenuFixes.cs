using HarmonyLib;
using UnityEngine;

namespace BearSimCommunityPatch;

[HarmonyPatch(typeof(GUI_StartScreen))]
internal class MainMenuFixes
{
    [HarmonyPatch(typeof(GUI_StartScreen), "Start")]
    [HarmonyPostfix]
    private static void Start_Postfix(GUI_StartScreen __instance)
    {
        GameObject startScreenManager = GameObject.Find("StartScreenManager");

        if (startScreenManager != null)
        {
            // Fixes the main menu theme not looping
            startScreenManager.GetComponent<AudioSource>().loop = true;
        }
    }

    [HarmonyPatch(typeof(GUI_StartScreen), "OptionButton")]
    [HarmonyPostfix]
    private static void OptionButton_Postfix(GUI_StartScreen __instance)
    {
        if (__instance is GUI_StartScreen startScreen)
        {
            // Makes the Kickstarter backer code entry usable again
            Transform optionsMenuTransform = startScreen.optionGUI.transform;
            GameObject backerCodeEntry = optionsMenuTransform.GetChild(4).gameObject;
            backerCodeEntry.SetActive(true);
        }
    }
}