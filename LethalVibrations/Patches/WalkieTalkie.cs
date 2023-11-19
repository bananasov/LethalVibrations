using BepInEx.Logging;
using HarmonyLib;

namespace LethalVibrations.Patches;

internal class WalkieTalkiePatches
{
    internal static ManualLogSource Logger { get; set; }

    public static void Init(ManualLogSource logger)
    {
        Logger = logger;
    }

    [HarmonyPatch(typeof(WalkieTalkie), "ItemActivate")]
    [HarmonyPostfix]
    static void ItemActivatePatch()
    {
        
    }
}