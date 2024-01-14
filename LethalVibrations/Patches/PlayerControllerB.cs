using GameNetcodeStuff;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class PlayerControllerBPatches
    {
        
        [HarmonyPatch(typeof(PlayerControllerB), "DamagePlayer")]
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        private static void DamagePlayerPatch(PlayerControllerB __instance, int damageNumber)
        {
            if (!__instance.IsOwner)
                return;

            var damage = (float)damageNumber;
            Plugin.Mls.LogDebug($"DamagePlayer got called: {damage} ({damage / 100f})");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateDamageReceivedEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevicesWithDuration((damage / 100f) + Config.VibrateDamageReceivedAmplifier.Value, Config.VibrateDamageReceivedDuration.Value);
            }
        }

        [HarmonyPatch(typeof(PlayerControllerB), "KillPlayer")]
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        private static void KillPlayerPatch(PlayerControllerB __instance)
        {
            if (!__instance.IsOwner)
                return;

            Plugin.Mls.LogDebug($"KillPlayer got called");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateKilledEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevicesWithDuration(Config.VibrateKilledStrength.Value, Config.VibrateKilledDuration.Value);
            }
        }
    }
}
