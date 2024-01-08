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
                    Plugin.DeviceManager.VibrateConnectedDevices(0.3f + Config.VibrateScreenShakeAmplifier.Value, Config.VibrateScreenShakeDuration.Value);
                    return;
                case ScreenShakeType.Big:
                    Plugin.DeviceManager.VibrateConnectedDevices(0.6f + Config.VibrateScreenShakeAmplifier.Value, Config.VibrateScreenShakeDuration.Value);
                    return;
                case ScreenShakeType.Long:
                    Plugin.DeviceManager.VibrateConnectedDevices(0.5f + Config.VibrateScreenShakeAmplifier.Value, Config.VibrateScreenShakeDuration.Value + 0.6f);
                    return;
                case ScreenShakeType.VeryStrong:
                    Plugin.DeviceManager.VibrateConnectedDevices(0.9f + Config.VibrateScreenShakeAmplifier.Value, Config.VibrateScreenShakeDuration.Value + 0.3f);
                    return;
                default:
                    return;
            }
        }

        [HarmonyPatch(typeof(HUDManager), "PingScan_performed")]
        [HarmonyPostfix]
        private static void PingScan_performedPatch()
        {
            Plugin.Mls.LogDebug($"PingScan_performed got called");

            if (Plugin.DeviceManager.IsConnected() && Config.PingScanEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(Config.PingScanStrength.Value, Config.PingScanDuration.Value);
            }
        }
    }
}
