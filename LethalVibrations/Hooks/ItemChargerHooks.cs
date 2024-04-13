using LethalVibrations.Buttplug;
using LethalVibrations.Utils;

namespace LethalVibrations.Hooks;

public class ItemChargerHooks
{
    [PatchInit]
    public static void Init()
    {
        LethalVibrations.Logger.LogInfo("Patching ItemCharger functions.");
        
        On.ItemCharger.ChargeItem += ItemChargerOnChargeItem;
    }

    private static void ItemChargerOnChargeItem(On.ItemCharger.orig_ChargeItem orig, ItemCharger self)
    {
        orig(self);

        if (LethalVibrations.DeviceManager.IsConnected() && Config.ItemCharge.Enabled!.Value)
        {
            LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(Config.ItemCharge.Strength!.Value,
                Config.ItemCharge.Duration!.Value);
        }
    }
}