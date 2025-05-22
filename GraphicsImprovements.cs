using HarmonyLib;
using UnityEngine;

namespace BearSimCommunityPatch;

[HarmonyPatch(typeof(SceneManager))]
internal class GraphicsImprovements
{
    [HarmonyPatch(typeof(SceneManager), "Start")]
    [HarmonyPostfix]
    private static void Start_Postfix()
    {
        // Only apply to High/Ultimate graphics quality
        if (QualitySettings.GetQualityLevel() >= 2)
        {
            Terrain terrain = GameObject.Find("Terrain").GetComponent<Terrain>();

            if (terrain != null)
            {
                terrain.detailObjectDistance = 150; // Reduce detail object pop-in (original value is 50)
            }
        }
    }
}