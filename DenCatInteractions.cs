using HarmonyLib;
using UnityEngine;

namespace BearSimCommunityPatch;

[HarmonyPatch(typeof(AnimalBuddyCat))]
internal class DenCatInteractions
{
    // private static bool isCatHovered = false;

    private static void AddCatInfoBox(GameObject cat, Vector3 collisionCenter, float collisionRadius, float collisionHeight)
    {
        objectGeneral catInfo = cat.GetComponent<objectGeneral>() ?? cat.AddComponent<objectGeneral>();
        // catInfo.objectID = "dencat";
        catInfo.objectName = "Cat";
        catInfo.objectInfo = "What nefarious thoughts might lie behind this steely gaze?";
        catInfo.objectWhat = "Animal Buddy";
        catInfo.examineStat = "[00FF00]+1 Rat Catching";
        // catInfo.button1 = " Pet";
        // catInfo.bearPoints = "";

        CapsuleCollider collider = cat.GetComponent<CapsuleCollider>() ?? cat.AddComponent<CapsuleCollider>();
        collider.center = collisionCenter;
        collider.radius = collisionRadius;
        collider.height = collisionHeight;
    }

    [HarmonyPatch(typeof(AnimalBuddyCat), "Start")]
    [HarmonyPostfix]
    private static void CatStart_Postfix(AnimalBuddyCat __instance)
    {
        if (__instance is AnimalBuddyCat cat)
        {
            AddCatInfoBox(cat.catSleeping, new Vector3(0f, 0.75f, -0.15f), 0.85f, 0.5f);
            AddCatInfoBox(cat.catCleaning, new Vector3(0f, 0.75f, -0.05f), 1f, 1f);
            AddCatInfoBox(cat.catStaringAtBunny, new Vector3(0f, 0.75f, -0.25f), 0.93f, 1f);
            AddCatInfoBox(cat.catSitting, new Vector3(0f, 0.5f, 0f), 0.83f, 1f);
            // AddCatInfoBox(cat.catSleepingOnBed, new Vector3(0f, 0.75f, -0.25f), 0.93f, 1); // Unused
            AddCatInfoBox(cat.catEating, new Vector3(0f, 0.75f, 0.15f), 1f, 1f);
            AddCatInfoBox(cat.catPlaying, new Vector3(0.9f, 0.75f, 1.25f), 1f, 1f); // This one ends up with a bit of a strange offset for some reason
            AddCatInfoBox(cat.catOnSpeaker, new Vector3(0f, 0.5f, 0f), 0.83f, 0.5f);
        }
    }
    /*
    [HarmonyPatch(typeof(AnimalBuddyCat), "Update")]
    [HarmonyPrefix]
    private static void CatUpdate_Prefix()
    {
        if (isCatHovered)
        {
            if (!FPSPlayer.aMainPlayer.isPaused)
            {
                if (Input.GetKeyDown(FPSPlayer.aMainPlayer.use))
                {
                    BearSounds.soundsBear.PlayVarious("meow");
                }
            }
            else
            {
                HUDMain.guiHud.infoBoxDeactivate();
            }
        }
    }

    [HarmonyPatch(typeof(objectCollectable), "OnMouseOver")]
    [HarmonyPrefix]
    private static bool OnMouseOver_Prefix(objectCollectable __instance, ref bool ___dontDouble, ref bool ___mouseExit, ref bool ___playSoundOnce, ref bool ___withinDistance)
    {
        // Hijacking the collectable infobox because there's no good way to make a fully custom one through runtime patches
        if (__instance is objectCollectable item && item.objectID == "dencat")
        {
            float num = Vector3.Distance(item.thePlayer.position, item.transform.position);

            if (num < item.distanceFrom && !___dontDouble)
            {
                isCatHovered = true;
                ___mouseExit = true;
                ___withinDistance = true;
                ___dontDouble = true;

                if (___playSoundOnce)
                {
                    BearSounds.soundsBear.HoverSound();
                    ___playSoundOnce = false;
                }

                HUDMain.guiHud.infoBoxActivate(item.objectName, item.objectInfo, item.objectWhat, item.examineStat, "", "", "", item.button1, "null");
            }

            if (num > item.distanceFrom && ___dontDouble)
            {
                HUDMain.guiHud.infoBoxDeactivate();

                isCatHovered = false;
                ___playSoundOnce = true;
                ___dontDouble = false;
                ___mouseExit = false;
                ___withinDistance = false;
            }

            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(objectCollectable), "OnMouseExit")]
    [HarmonyPostfix]
    private static void OnMouseExit_Postfix(objectCollectable __instance)
    {
        if (__instance is objectCollectable item && item.objectID == "dencat")
        {
            isCatHovered = false;
        }
    }
    */
}