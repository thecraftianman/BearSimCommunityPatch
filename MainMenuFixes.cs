using HarmonyLib;
using UnityEngine;

namespace BearSimCommunityPatch;

[HarmonyPatch(typeof(GUI_StartScreen))]
internal class MainMenuFixes
{
    [HarmonyPatch(typeof(GUI_StartScreen), "Start")]
    [HarmonyPostfix]
    private static void Start_Postfix()
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
            Transform optionsMenuTransform = startScreen.optionGUI.transform;

            // Makes the Kickstarter backer code entry usable again
            GameObject backerCodeEntry = optionsMenuTransform.GetChild(4).gameObject;
            backerCodeEntry.SetActive(true);

            /*
            // Moves the help button to a slightly better spot in the menu
            // TODO: Figure out why this doesn't work as expected
            Transform helpButtonTransform = optionsMenuTransform.GetChild(1);
            helpButtonTransform.gameObject.SetActive(true);
            helpButtonTransform.localPosition = new Vector3(0, -482, 0);
            */
        }
    }
}