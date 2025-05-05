using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace BearSimUnofficialPatch;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
public class UnofficialPatchPlugin : BaseUnityPlugin
{
    private const string PluginAuthor = "thecraftianman";
    private const string PluginName = "Bear Simulator Unofficial Patch";
    private const string PluginGUID = PluginAuthor + ".bearsimunofficialpatch";
    private const string PluginVersion = "1.0.0";

    internal static new ManualLogSource Logger;
    private static readonly Harmony Harmony = new Harmony(PluginGUID);

    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Harmony.PatchAll();
        Logger.LogInfo($"Plugin {PluginGUID} is loaded!");
    }
}
