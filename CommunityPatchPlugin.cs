using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace BearSimCommunityPatch;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
public class CommunityPatchPlugin : BaseUnityPlugin
{
    private const string PluginAuthor = "thecraftianman";
    private const string PluginName = "Bear Simulator Community Patch";
    private const string PluginGUID = PluginAuthor + ".bearsimcommunitypatch";
    private const string PluginVersion = "1.0.1";

    internal static new ManualLogSource Logger;
    private static readonly Harmony Harmony = new(PluginGUID);

    // This function doesn't get directly called in the project, so let's silence the warning about it
    #pragma warning disable IDE0051
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Harmony.PatchAll();
        Logger.LogInfo($"Plugin {PluginGUID} v{PluginVersion} is loaded!");
    }
    #pragma warning restore IDE0051
}
