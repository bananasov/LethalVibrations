using LethalHaptics.Utils;

namespace LethalHaptics.Hooks;

// ReSharper disable once UnusedType.Global
public class NoiseMakerPropHooks
{
    [ButtplugPatch]
    // ReSharper disable once UnusedMember.Global
    public static void Initialize()
    {
        On.NoisemakerProp.ItemActivate += NoisemakerPropOnItemActivate;
    }

    private static void NoisemakerPropOnItemActivate(On.NoisemakerProp.orig_ItemActivate orig, NoisemakerProp self, bool used, bool buttonDown)
    {
        orig(self, used, buttonDown);
        
        if (self.itemProperties.itemName != "Airhorn")
            return;
    }
}