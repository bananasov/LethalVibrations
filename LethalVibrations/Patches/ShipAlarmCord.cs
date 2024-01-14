using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    public class ShipAlarmCordPatches
    {
        [HarmonyPatch(typeof(ShipAlarmCord), "HoldCordDown")]
        [HarmonyPostfix]
        private static void HoldCordDownPatch()
        {
            Plugin.Mls.LogDebug($"ShipAlarmCord.HoldCordDown got called");
            
            // I am well aware that `HoldCordDown` gets called SO MANY FUCKING TIMES.
            if (Plugin.DeviceManager.IsConnected() && Config.ShipHornEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(Config.ShipHornStrength.Value);
            }
        }
        
        [HarmonyPatch(typeof(ShipAlarmCord), "StopHorn")]
        [HarmonyPostfix]
        private static void StopHornPatch()
        {
            Plugin.Mls.LogDebug($"ShipAlarmCord.StopHorn got called");
            
            if (Plugin.DeviceManager.IsConnected() && Config.ShipHornEnabled.Value)
            {
                Plugin.DeviceManager.StopConnectedDevices();
            }
        }
    }
}