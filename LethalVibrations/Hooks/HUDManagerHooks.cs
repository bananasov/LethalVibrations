using System;
using LethalVibrations.Buttplug;
using LethalVibrations.Utils;

namespace LethalVibrations.Hooks;

public class HUDManagerHooks
{
    [PatchInit]
    public static void Init()
    {
        LethalVibrations.Logger.LogInfo("Patching HUDManager functions.");
        
        On.HUDManager.ShakeCamera += HUDManagerOnShakeCamera;
    }

    private static void HUDManagerOnShakeCamera(On.HUDManager.orig_ShakeCamera orig, HUDManager self,
        ScreenShakeType shakeType)
    {
        orig(self, shakeType);

        if (!LethalVibrations.DeviceManager.IsConnected() || !Config.ScreenShake.Enabled!.Value) return;
        switch (shakeType)
        {
            case ScreenShakeType.Small:
                LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(0.3f, 0.3f);
                break;
            case ScreenShakeType.Big:
                LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(0.6f, 0.3f);
                break;
            case ScreenShakeType.Long:
                LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(0.3f, 1.0f);
                break;
            case ScreenShakeType.VeryStrong:
                LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(1.0f, 0.6f);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(shakeType), shakeType, null);
        }
    }
}