using LethalHaptics.Utils;

namespace LethalHaptics.Hooks;

// ReSharper disable once UnusedType.Global
public class ShipAlarmCordHooks
{
    [ButtplugPatch]
    // ReSharper disable once UnusedMember.Global
    public static void Initialize()
    {
        Plugin.Log.LogDebug("Hooking Ship alarm cord functions");
    }
}