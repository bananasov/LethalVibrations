using LethalHaptics.Utils;

namespace LethalHaptics.Hooks;

public class NoiseMakerPropHooks : IHook
{
    [ButtplugPatch]
    public void Initialize()
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