using LethalHaptics.Utils;

namespace LethalHaptics.Hooks;

// ReSharper disable once UnusedType.Global
public class StunGrenadeItemHooks
{
    [ButtplugPatch]
    // ReSharper disable once UnusedMember.Global
    public static void Initialize()
    {
        Plugin.Log.LogDebug("Hooking Stun Grenade functions");
    }
}