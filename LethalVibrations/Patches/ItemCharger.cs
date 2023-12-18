using BepInEx.Logging;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class ItemChargerPatches
    {
        private static ManualLogSource Logger { get; set; }
        
        public static void Init(ManualLogSource logger)
        {
            Logger = logger;
        }
        
        [HarmonyPatch(typeof(ItemCharger), "ChargeItem")]
        [HarmonyPostfix]
        private static void ChargeItemPatch()
        {
            Logger.LogDebug($"ChargeItem got called");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateItemChargerChargeEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(Config.VibrateItemChargerChargeStrength.Value, Config.VibrateItemChargerChargeDuration.Value);
            }
        }
    }   
}