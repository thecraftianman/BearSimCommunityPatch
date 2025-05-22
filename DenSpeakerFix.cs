using HarmonyLib;
using UnityEngine;

namespace BearSimCommunityPatch;

[HarmonyPatch(typeof(AnimalBud_Raven))]
internal class DenSpeakerFix
{
    private static GameObject speaker;

    [HarmonyPatch(typeof(DenManager), "Start")]
    [HarmonyPrefix]
    private static void Start_Postfix(DenManager __instance)
    {
        if (__instance is DenManager denManager)
        {
            speaker = denManager.speakers.transform.GetChild(1).gameObject;
        }
    }

    [HarmonyPatch(typeof(AnimalBud_Raven), "SwitchSong")]
    [HarmonyPostfix]
    private static void SwitchSong_Postfix(AnimalBud_Raven __instance, ref int ___songNumber)
    {
        if (__instance is AnimalBud_Raven raven)
        {
            if (___songNumber == 1)
                speaker.GetComponent<AudioSource>().clip = raven.song1;
            if (___songNumber == 2)
                speaker.GetComponent<AudioSource>().clip = raven.song2;
            if (___songNumber == 3)
                speaker.GetComponent<AudioSource>().clip = raven.song3;
            if (___songNumber == 0)
                speaker.GetComponent<AudioSource>().clip = raven.song4;

            speaker.GetComponent<AudioSource>().Play();
        }
    }

    [HarmonyPatch(typeof(AnimalBud_Raven), "StopMusic")]
    [HarmonyPostfix]
    private static void StopMusic_Postfix()
    {
        speaker.GetComponent<AudioSource>().Stop();
    }
}