﻿using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class NoisemakerPropPatches
    {
        [HarmonyPatch(typeof(NoisemakerProp), "ItemActivate")]
        [HarmonyPostfix]
        private static void ItemActivatePatch(NoisemakerProp __instance)
        {
            Plugin.Mls.LogDebug($"NoisemakerProp.ItemActivate got called: {__instance.itemProperties.itemName}");

            if (__instance.itemProperties.itemName != "Airhorn")
                return;

            if (Plugin.DeviceManager.IsConnected() && Config.AirhornEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevicesWithDuration(Config.AirhornStrength.Value, Config.AirhornDuration.Value);
            }
        }
    }   
}