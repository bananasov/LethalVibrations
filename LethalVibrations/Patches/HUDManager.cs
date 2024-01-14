using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class HUDManagerPatches
    {
        [HarmonyPatch(typeof(HUDManager), "ShakeCamera")]
        [HarmonyPostfix]
        private static void ShakeCameraPatch(ScreenShakeType shakeType)
        {
            Plugin.Mls.LogDebug($"ShakeCamera {shakeType} got called");

            if (!Plugin.DeviceManager.IsConnected() || !Config.VibrateScreenShakeEnabled.Value) return;
            
            switch (shakeType)
            {
                case ScreenShakeType.Small:
                    Plugin.DeviceManager.VibrateConnectedDevicesWithDuration(0.3f + Config.VibrateScreenShakeAmplifier.Value, Config.VibrateScreenShakeDuration.Value);
                    return;
                case ScreenShakeType.Big:
                    Plugin.DeviceManager.VibrateConnectedDevicesWithDuration(0.6f + Config.VibrateScreenShakeAmplifier.Value, Config.VibrateScreenShakeDuration.Value);
                    return;
                case ScreenShakeType.Long:
                    Plugin.DeviceManager.VibrateConnectedDevicesWithDuration(0.5f + Config.VibrateScreenShakeAmplifier.Value, Config.VibrateScreenShakeDuration.Value + 0.6f);
                    return;
                case ScreenShakeType.VeryStrong:
                    Plugin.DeviceManager.VibrateConnectedDevicesWithDuration(0.9f + Config.VibrateScreenShakeAmplifier.Value, Config.VibrateScreenShakeDuration.Value + 0.3f);
                    return;
                default:
                    return;
            }
        }

        [HarmonyPatch(typeof(HUDManager), "DisplayNewDeadline")]
        [HarmonyPostfix]
        private static void DisplayNewDeadlinePatch()
        {
            Plugin.Mls.LogDebug($"DisplayNewDeadline got called");
            
            if (Plugin.DeviceManager.IsConnected() && Config.QuotaReachedEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevicesWithDuration(Config.QuotaReachedStrength.Value, Config.QuotaReachedDuration.Value);
            }
        }

        [HarmonyPatch(typeof(HUDManager), "PingScan_performed")]
        [HarmonyPostfix]
        private static void PingScan_performedPatch()
        {
            Plugin.Mls.LogDebug($"PingScan_performed got called");

            if (Plugin.DeviceManager.IsConnected() && Config.PingScanEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevicesWithDuration(Config.PingScanStrength.Value, Config.PingScanDuration.Value);
            }
        }
    }
}
