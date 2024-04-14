using LethalVibrations.Buttplug;
using LethalVibrations.Utils;

namespace LethalVibrations.Hooks;

public class GrabbableObjectHooks
{
    [PatchInit]
    public static void Init()
    {
        LethalVibrations.Logger.LogInfo("Patching GrabbableObject functions.");

        On.GrabbableObject.GrabItemOnClient += GrabbableObjectOnGrabItemOnClient;
    }

    private static void GrabbableObjectOnGrabItemOnClient(On.GrabbableObject.orig_GrabItemOnClient orig,
        GrabbableObject self)
    {
        orig(self);

        if (!self.itemProperties.isScrap)
            return;

        if (self.isInShipRoom)
            return;

        if (LethalVibrations.DeviceManager.IsConnected() && Config.ScrapPickup.Enabled!.Value)
        {
            LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(Config.ScrapPickup.Strength!.Value,
                Config.ScrapPickup.Duration!.Value);
        }
    }
}