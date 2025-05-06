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
        if (__instance is regionSpooky Woods)
        {
            CommunityPatchPlugin.Logger.LogInfo("Fixing shadow pigs...");

            GameObject Pig1 = Woods.spookyPig1;
            GameObject Pig2 = Woods.spookyPig2;
            GameObject Pig3 = Woods.spookyPig3;

            // Set the correct layer on all shadow pigs
            // This allows them to be hit properly like all other animals
            int creaturesLayer = 24;
            Pig1.layer = creaturesLayer;
            Pig2.layer = creaturesLayer;
            Pig3.layer = creaturesLayer;
        }
    }
}