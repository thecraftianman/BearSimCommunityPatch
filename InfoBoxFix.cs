using HarmonyLib;

namespace BearSimCommunityPatch;

[HarmonyPatch(typeof(objectFood))]
internal class InfoBoxFix
{
    [HarmonyPatch(typeof(objectFood), "Start")]
    [HarmonyPostfix]
    private static void Start_Postfix(objectFood __instance)
    {
        if (__instance is objectFood food && food.objectButton2 == "Null")
        {
            // Fixes some food items displaying a secondary option of "Null" (like blackberries in the lake)
            food.objectButton2 = "null";
        }
    }

    [HarmonyPatch(typeof(HUDMain), "displaySpecial")]
    [HarmonyPostfix]
    private static void DisplaySpecial_Postfix(ref string whatisneeded, HUDMain __instance)
    {
        if (__instance is HUDMain hud)
        {
            if (whatisneeded == "thebeehive")
            {
                // Fixes the beehive displaying an empty secondary option
                UILabel secondaryButtonLabel = hud.hudInfoButton2Obj.GetComponent<UILabel>();
                secondaryButtonLabel.text = string.Empty;
            }

            if (whatisneeded == "balalala")
            {
                // Fixes the button not being listed in the text
                UILabel primaryButtonLabel = hud.hudInfoButton1Obj.GetComponent<UILabel>();
                primaryButtonLabel.text = controlScheme.theControls.eKey + "): Play!";
            }

            if (whatisneeded == "valve")
            {
                // Fixes the button not being listed in the text
                UILabel primaryButtonLabel = hud.hudInfoButton1Obj.GetComponent<UILabel>();
                primaryButtonLabel.text = controlScheme.theControls.eKey + "): Place Valve";
            }
        }
    }

    [HarmonyPatch(typeof(theBEES), "Start")]
    [HarmonyPostfix]
    private static void BeesStart_Postfix(theBEES __instance)
    {
        if (__instance is theBEES bees)
        {
            // Fixing a minor typo in this string
            bees.doorInfo = "Test your luck at the beehive! Will you get a random stat or get stung? Who knows!";
        }
    }
}