using BepInEx.Logging;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class HUDManagerPatches
    {
        private static ManualLogSource Logger { get; set; }

        public static void Init(ManualLogSource logger)
        {
            Logger = logger;
        }

        [HarmonyPatch(typeof(HUDManager), "ShakeCamera")]
        [HarmonyPostfix]
        static void ShakeCameraPatch(ScreenShakeType shakeType)
        {
            Logger.LogInfo($"ShakeCamera {shakeType} got called");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateShakeScreenEnabled.Value)
            {
                switch (shakeType)
            	{
            		case ScreenShakeType.Small:
            			Plugin.DeviceManager.VibrateConnectedDevices(0.3f + Config.VibrateShakeScreenAmplifier.Value, Config.VibrateShakeScreenDuration.Value);
            			return;
            		case ScreenShakeType.Big:
                        Plugin.DeviceManager.VibrateConnectedDevices(0.6f + Config.VibrateShakeScreenAmplifier.Value, Config.VibrateShakeScreenDuration.Value);
            			return;
            		case ScreenShakeType.Long:
                        Plugin.DeviceManager.VibrateConnectedDevices(0.5f + Config.VibrateShakeScreenAmplifier.Value, Config.VibrateShakeScreenDuration.Value + 0.6f);
            			return;
            		case ScreenShakeType.VeryStrong:
            			Plugin.DeviceManager.VibrateConnectedDevices(0.9f + Config.VibrateShakeScreenAmplifier.Value, Config.VibrateShakeScreenDuration.Value + 0.3f);
            			return;
            	}
            }
        }

        [HarmonyPatch(typeof(HUDManager), "PingScan_performed")]
        [HarmonyPostfix]
        static void PingScan_performedPatch()
        {
            Logger.LogInfo($"PingScan_performed got called");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateShakeScreenEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(0.3f + Config.PingScanAmplifier.Value, Config.PingScanDuration.Value);
            }
        }
    }
}