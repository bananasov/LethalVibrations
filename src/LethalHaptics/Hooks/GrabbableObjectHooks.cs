using LethalHaptics.Utils;

namespace LethalHaptics.Hooks;

// ReSharper disable once UnusedType.Global
public class GrabbableObjectHooks : IHook
{
    [ButtplugPatch]
    public void Initialize()
    {
        On.GrabbableObject.GrabItemOnClient += GrabbableObjectOnGrabItemOnClient;
    }

    private static void GrabbableObjectOnGrabItemOnClient(On.GrabbableObject.orig_GrabItemOnClient orig, GrabbableObject self)
    {
        orig(self);

        if (!self.itemProperties.isScrap) return;
        if (self.isInShipRoom) return;

        if (Plugin.DeviceManager.IsConnected())
        {
            Plugin.Log.LogDebug("Scrap picked up :3");
        }
    }
}