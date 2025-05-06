using HarmonyLib;

namespace BearSimCommunityPatch;

[HarmonyPatch(typeof(FPSRigidBodyWalker))]
internal class JumpDelayFix
{
    [HarmonyPatch(typeof(FPSRigidBodyWalker), "Start")]
    [HarmonyPostfix]
    private static void Start_Postfix(FPSRigidBodyWalker __instance)
    {
        if (__instance is FPSRigidBodyWalker PlayerWalker)
        {
            CommunityPatchPlugin.Logger.LogInfo("Fixing jump delay...");

            // The code appears to set this value to 0.35f by default, but it gets set
            // to 0.7f somehow elsewhere. Lowering this value makes jumping feel a bit
            // more responsive.
            PlayerWalker.antiBunnyHopFactor = 0.15f;
        }
    }
}