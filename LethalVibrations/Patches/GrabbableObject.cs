using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class GrabbableObjectPatches
    {
        [HarmonyPatch(typeof(GrabbableObject), "GrabItemOnClient")]
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        private static void GrabItemOnClientPatch(GrabbableObject __instance)
        {
            if (!__instance.itemProperties.isScrap)
                return;

            Plugin.Mls.LogDebug("GrabItemOnClient called");
            if (Plugin.DeviceManager.IsConnected() && Config.Rewarding.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(Config.VibrateKilledStrength.Value, Config.VibrateKilledDuration.Value);
            }
        }
    }
}
