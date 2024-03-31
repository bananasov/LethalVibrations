using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    public class StunGrenadeItemPatches
    {
        [HarmonyPatch(typeof(StunGrenadeItem), "StunExplosion")]
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        private static void StunExplosionPatch(StunGrenadeItem __instance)
        {
            Plugin.Mls.LogDebug("StunExplosion got called");
            
            if (Plugin.DeviceManager.IsConnected() && Config.FlashbangEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevicesWithDuration(Config.FlashbangStrength.Value, 0.3f);
            }
        }
    }
}

