using HarmonyLib;

namespace BearSimUnofficialPatch;

[HarmonyPatch(typeof(regionSpooky))]
internal class ShadowPigFix
{
    [HarmonyPatch(typeof(regionSpooky), "Awake")]
    [HarmonyPostfix]
    private static void Start_Postfix(regionSpooky __instance)
    {
        if (__instance is regionSpooky woods)
        {
            UnofficialPatchPlugin.Logger.LogInfo($"Fixing shadow pigs...");

            // Set the correct layer on all shadow pigs
            // This allows them to be hit properly like all other animals
            int creaturesLayer = 24;
            woods.spookyPig1.layer = creaturesLayer;
            woods.spookyPig2.layer = creaturesLayer;
            woods.spookyPig3.layer = creaturesLayer;
        }
    }
}