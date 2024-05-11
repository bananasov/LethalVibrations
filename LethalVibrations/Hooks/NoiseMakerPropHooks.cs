using LethalVibrations.Buttplug;
using LethalVibrations.Utils;
using UnityEngine;

namespace LethalVibrations.Hooks;

public class NoiseMakerPropHooks
{
    [PatchInit]
    public static void Init()
    {
        LethalVibrations.Logger.LogInfo("Patching NoiseMakerProp functions.");

        On.NoisemakerProp.ItemActivate += NoisemakerPropOnItemActivate;
    }

    private static void NoisemakerPropOnItemActivate(On.NoisemakerProp.orig_ItemActivate orig, NoisemakerProp self,
        bool used, bool buttonDown)
    {
        orig(self, used, buttonDown);

        if (self.itemProperties.itemName != "Airhorn")
            return;

        // TODO: Check distance to item

        if (LethalVibrations.DeviceManager.IsConnected() && Config.Airhorn.Enabled!.Value)
        {
            LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(Config.Airhorn.Strength!.Value,
                Config.Airhorn.Duration!.Value);
        }
    }
}