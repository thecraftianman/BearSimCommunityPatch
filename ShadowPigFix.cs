using HarmonyLib;
using UnityEngine;

namespace BearSimCommunityPatch;

[HarmonyPatch(typeof(regionSpooky))]
internal class ShadowPigFix
{
    [HarmonyPatch(typeof(regionSpooky), "Awake")]
    [HarmonyPostfix]
    private static void Start_Postfix(regionSpooky __instance)
    {
        // TODO: Lower local positions of pigs to stop them from floating above the ground
        if (__instance is regionSpooky woods)
        {
            CommunityPatchPlugin.Logger.LogInfo("Fixing shadow pigs...");

            GameObject pig1 = woods.spookyPig1;
            GameObject pig2 = woods.spookyPig2;
            GameObject pig3 = woods.spookyPig3;

            // Set the correct layer on all shadow pigs
            // This allows them to be hit properly like all other animals
            int creaturesLayer = 24;
            pig1.layer = creaturesLayer;
            pig2.layer = creaturesLayer;
            pig3.layer = creaturesLayer;
        }
    }
}