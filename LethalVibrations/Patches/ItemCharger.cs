using BepInEx.Logging;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class ItemChargerPatches
    {
        [HarmonyPatch(typeof(ItemCharger), "ChargeItem")]
        [HarmonyPostfix]
        private static void ChargeItemPatch()
        {
            Plugin.Mls.LogDebug($"ChargeItem got called");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateItemChargerChargeEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(Config.VibrateItemChargerChargeStrength.Value, Config.VibrateItemChargerChargeDuration.Value);
            }
        }
    }   
}