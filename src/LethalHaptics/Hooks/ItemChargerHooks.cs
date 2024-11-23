using LethalHaptics.Utils;

namespace LethalHaptics.Hooks;

// ReSharper disable once UnusedType.Global
public static class ItemChargerHooks
{
    [ButtplugPatch]
    // ReSharper disable once UnusedMember.Global
    public static void Initialize()
    {
        Plugin.Log.LogDebug("Patching Item Charger functions");
        
        On.ItemCharger.ChargeItem += ItemChargerOnChargeItem;
    }

    private static void ItemChargerOnChargeItem(On.ItemCharger.orig_ChargeItem orig, ItemCharger self)
    {
        orig(self);

        if (Plugin.DeviceManager.IsConnected())
        {
            Plugin.Log.LogDebug("Vibrating ye plugs");
        }
    }
}