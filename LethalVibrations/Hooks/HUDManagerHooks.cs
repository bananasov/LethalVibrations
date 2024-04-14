using System;
using LethalVibrations.Buttplug;
using LethalVibrations.Utils;
using UnityEngine.InputSystem;

namespace LethalVibrations.Hooks;

public class HUDManagerHooks
{
    [PatchInit]
    public static void Init()
    {
        LethalVibrations.Logger.LogInfo("Patching HUDManager functions.");

        On.HUDManager.ShakeCamera += HUDManagerOnShakeCamera;
        On.HUDManager.PingScan_performed += HUDManagerOnPingScan_performed;
        On.HUDManager.DisplayNewDeadline += HUDManagerOnDisplayNewDeadline;
    }

    private static void HUDManagerOnDisplayNewDeadline(On.HUDManager.orig_DisplayNewDeadline orig, HUDManager self,
        int overtimeBonus)
    {
        orig(self, overtimeBonus);

        if (LethalVibrations.DeviceManager.IsConnected() && Config.QuotaReached.Enabled!.Value)
        {
            LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(Config.QuotaReached.Strength!.Value,
                Config.QuotaReached.Duration!.Value);
        }
    }

    private static void HUDManagerOnPingScan_performed(On.HUDManager.orig_PingScan_performed orig, HUDManager self,
        InputAction.CallbackContext context)
    {
        orig(self, context);

        if (LethalVibrations.DeviceManager.IsConnected() && Config.Scanning.Enabled!.Value)
        {
            LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(Config.Scanning.Strength!.Value,
                Config.Scanning.Duration!.Value);
        }
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