using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace BearSimCommunityPatch;

[HarmonyPatch(typeof(BearSounds))]
internal class SoundCutoffFix
{
    // Many sounds that are run on the player use the Play() method instead of PlayOneShot(),
    // which can cause them to interrupt each other instead of playing simultaneously. The
    // method overrides here will fix that and refactor the original methods to be much cleaner.
    private struct SoundData
    {
        public AudioClip[] AudioClips;
        public float Volume = 1f;
        public bool ShouldStopPrevious = false;

        public SoundData(AudioClip audioClip, float volume)
        {
            AudioClips = [audioClip];
            Volume = volume;
        }

        public SoundData(AudioClip audioClip)
        {
            AudioClips = [audioClip];
        }

        public SoundData(AudioClip[] audioClips, float volume)
        {
            AudioClips = audioClips;
            Volume = volume;
        }

        public SoundData(AudioClip[] audioClips)
        {
            AudioClips = audioClips;
        }

        public SoundData(AudioClip audioClip, float volume, bool shouldStopPrevious)
        {
            AudioClips = [audioClip];
            Volume = volume;
            ShouldStopPrevious = shouldStopPrevious;
        }

        public SoundData(AudioClip[] audioClips, float volume, bool shouldStopPrevious)
        {
            AudioClips = audioClips;
            Volume = volume;
            ShouldStopPrevious = shouldStopPrevious;
        }
    }

    private static bool dictionariesInitialized = false;
    private static readonly Dictionary<string, SoundData> attacksDictionary = [];
    private static readonly Dictionary<string, SoundData> variousDictionary = [];
    private static readonly Dictionary<string, SoundData> breakDictionary = [];

    [HarmonyPatch(typeof(BearSounds), "Start")]
    [HarmonyPostfix]
    private static void Start_Postfix(BearSounds __instance)
    {
        if (__instance is BearSounds sounds && !dictionariesInitialized)
        {
            // Attack sounds (used by PlayAttacks)
            attacksDictionary.Add("PSready", new SoundData(sounds.powerSwipeReady, 1f, true));
            attacksDictionary.Add("PScharge", new SoundData(sounds.powerSwipeCharge, 1f));
            attacksDictionary.Add("PSattack", new SoundData(sounds.powerSwipeAttack, 1f, true));
            attacksDictionary.Add("PScooldown", new SoundData(sounds.powerSwipeCooldown, 1f));
            attacksDictionary.Add("PSrecharged", new SoundData(sounds.powerSwipeRecharged, 1f));
            attacksDictionary.Add("regularSwipe", new SoundData(sounds.regularSwipe, 1f));

            // Various sounds (used by PlayVarious)
            variousDictionary.Add("bala", new SoundData(sounds.balalalala, 1f));
            variousDictionary.Add("crouch", new SoundData(sounds.crouch, 1f));
            variousDictionary.Add("collect", new SoundData(sounds.collectSomething, 1f));
            variousDictionary.Add("error", new SoundData(sounds.bearError, 0.5f));
            variousDictionary.Add("fall", new SoundData(sounds.fallDmg, 0.5f));
            variousDictionary.Add("ability", new SoundData(sounds.newAbility, 0.5f));
            variousDictionary.Add("heartbeat", new SoundData(sounds.heartBeats, 1f));
            variousDictionary.Add("unlock", new SoundData(sounds.unlockSomething, 0.5f));
            variousDictionary.Add("achievement", new SoundData(sounds.achievement, 0.3f));
            variousDictionary.Add("HoverButton", new SoundData(sounds.hoverButton, 0.2f));
            variousDictionary.Add("pswipeobject", new SoundData(sounds.pswipeObject, 0.8f));
            variousDictionary.Add("swipeobject", new SoundData(sounds.swipeObject, 0.8f));
            variousDictionary.Add("horrorcollect", new SoundData(sounds.horrorCollect, 0.4f));
            variousDictionary.Add("drink", new SoundData(sounds.eatSoundsLiquid, 1f, true));
            variousDictionary.Add("equip", new SoundData(sounds.equip, 0.5f, true));
            variousDictionary.Add("openmenu", new SoundData(sounds.openMenu, 0.7f, true));
            variousDictionary.Add("closemenu", new SoundData(sounds.closeMenu, 0.7f, true));
            variousDictionary.Add("newarea", new SoundData(sounds.newArea, 0.3f, true));
            variousDictionary.Add("smell", new SoundData(sounds.smell, 0.5f, true));
            variousDictionary.Add("spookfound", new SoundData(sounds.spookyFound, 1f));
            variousDictionary.Add("paper1", new SoundData(sounds.paper1, 1f));
            variousDictionary.Add("paper2", new SoundData(sounds.paper2, 1f));
            variousDictionary.Add("papercrumble", new SoundData(sounds.paperCrumble, 1f));
            variousDictionary.Add("violin", new SoundData(sounds.horrorViolin, 0.8f));
            variousDictionary.Add("rockcollect", new SoundData(sounds.rockCollect, 0.8f));
            variousDictionary.Add("bigfootishere", new SoundData(sounds.bigfootishere, 1f));
            variousDictionary.Add("toxic", new SoundData(sounds.toxic, 1f));
            variousDictionary.Add("woodcollect", new SoundData(sounds.woodCollect, 1f));
            variousDictionary.Add("backercollect", new SoundData(sounds.backerCollect, 1f));
            variousDictionary.Add("glasscollect", new SoundData(sounds.glassCollect, 1f));
            variousDictionary.Add("darkcollect", new SoundData(sounds.darkCollect, 1f));
            variousDictionary.Add("metalcollect", new SoundData(sounds.metalCollect, 1f));
            variousDictionary.Add("DJRaven", new SoundData(sounds.DJRaven, 1f));
            variousDictionary.Add("beesting", new SoundData(sounds.BeeSting, 1f));
            variousDictionary.Add("beelaugh", new SoundData(sounds.BeeLaugh, 1f));
            variousDictionary.Add("locked", new SoundData(sounds.locked, 1f));
            variousDictionary.Add("unlocked", new SoundData(sounds.unlocked, 1f));
            variousDictionary.Add("sleep", new SoundData(sounds.sleep, 1f));
            variousDictionary.Add("success", new SoundData(sounds.success, 1f));
            variousDictionary.Add("fail", new SoundData(sounds.fail, 1f));
            variousDictionary.Add("ghost", new SoundData(sounds.ghost, 1f));
            variousDictionary.Add("swipe", new SoundData(sounds.swipe, 1f));
            variousDictionary.Add("butterfly", new SoundData(sounds.butterfly, 1f));
            variousDictionary.Add("rip", new SoundData(sounds.paperRip, 1f));
            variousDictionary.Add("poof", new SoundData(sounds.poof, 1f));
            variousDictionary.Add("glass", new SoundData(sounds.glass, 1f));
            variousDictionary.Add("colorcollect", new SoundData(sounds.colorcollect, 1f));
            variousDictionary.Add("mystic", new SoundData(sounds.mystic, 1f));
            variousDictionary.Add("rekt", new SoundData(sounds.rekt, 1f));
            variousDictionary.Add("bush", new SoundData(sounds.bush, 0.6f));
            variousDictionary.Add("bubbles", new SoundData(sounds.bubbles, 1f));
            variousDictionary.Add("save", new SoundData(sounds.save, 0.5f));
            variousDictionary.Add("bearhit", new SoundData(sounds.bearHit, 1f));
            variousDictionary.Add("growl", new SoundData(sounds.bearGrowl, 1f));
            variousDictionary.Add("roar", new SoundData(sounds.bearRoar, 1f));

            // Material breaking sounds (used by PlayBreakWood)
            breakDictionary.Add("wood", new SoundData(sounds.breakWood, 1f));
            breakDictionary.Add("metal", new SoundData(sounds.metalBreak, 1f));
            breakDictionary.Add("rock", new SoundData(sounds.rockBreak, 1f));

            dictionariesInitialized = true;
        }
    }

    private static bool PlaySound(ref string soundName, Dictionary<string, SoundData> dictionary, BearSounds __instance, ref AudioClip ___theSound)
    {
        if (__instance is BearSounds sounds)
        {
            if (dictionary.TryGetValue(soundName, out SoundData soundData))
            {
                if (soundData.ShouldStopPrevious)
                {
                    sounds.GetComponent<AudioSource>().Stop();
                }

                ___theSound = soundData.AudioClips[Random.Range(0, soundData.AudioClips.Length)];
                sounds.GetComponent<AudioSource>().PlayOneShot(___theSound, soundData.Volume);

                return false;
            }
        }

        return true;
    }

    [HarmonyPatch(typeof(BearSounds), "PlayAttacks")]
    [HarmonyPrefix]
    private static bool PlayAttacks_Prefix(ref string theEvent, BearSounds __instance, ref AudioClip ___theSound)
    {
        return PlaySound(ref theEvent, attacksDictionary, __instance, ref ___theSound);
    }

    [HarmonyPatch(typeof(BearSounds), "PlayVarious")]
    [HarmonyPrefix]
    private static bool PlayVarious_Prefix(ref string whichSound, BearSounds __instance, ref AudioClip ___theSound)
    {
        return PlaySound(ref whichSound, variousDictionary, __instance, ref ___theSound);
    }

    [HarmonyPatch(typeof(BearSounds), "PlayBreakWood")]
    [HarmonyPrefix]
    private static bool PlayBreakWood_Prefix(ref string whichone, BearSounds __instance, ref AudioClip ___theSound)
    {
        return PlaySound(ref whichone, breakDictionary, __instance, ref ___theSound);
    }

    [HarmonyPatch(typeof(BearSounds), "PlaySmallDamage")]
    [HarmonyPrefix]
    private static bool PlaySmallDamage_Prefix(BearSounds __instance, ref AudioClip ___theSound)
    {
        if (__instance is BearSounds sounds)
        {
            ___theSound = sounds.smallDamage;
            sounds.GetComponent<AudioSource>().PlayOneShot(sounds.smallDamage, 1f);

            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(BearSounds), "PlayMagicMusic1")]
    [HarmonyPrefix]
    private static bool PlayMagicMusic1_Prefix(BearSounds __instance, ref AudioClip ___theSound)
    {
        if (__instance is BearSounds sounds)
        {
            ___theSound = sounds.magicMusic1;
            sounds.GetComponent<AudioSource>().PlayOneShot(sounds.magicMusic1, 1f);

            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(BearSounds), "PlayHorrorMusic1")]
    [HarmonyPrefix]
    private static bool PlayHorrorMusic1_Prefix(BearSounds __instance, ref AudioClip ___theSound)
    {
        if (__instance is BearSounds sounds)
        {
            ___theSound = sounds.horrorMusic1;
            sounds.GetComponent<AudioSource>().PlayOneShot(sounds.horrorMusic1, 1f);

            return false;
        }

        return true;
    }
}