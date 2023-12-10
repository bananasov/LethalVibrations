using BepInEx.Logging;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class ItemChargerPatches
    {
        internal static ManualLogSource Logger { get; set; }
        
        public static void Init(ManualLogSource logger)
        {
            Logger = logger;
        }
        
        [HarmonyPatch(typeof(ItemCharger), "ChargeItem")]
        [HarmonyPostfix]
        static void ChargeItemPatch()
        {
            Logger.LogInfo($"ChargeItem got called");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateItemChargerChargeEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(0.8f + Config.VibrateItemChargerChargeAmplifier.Value, Config.VibrateItemChargerChargeDuration.Value);
            }
        }
    }   
}