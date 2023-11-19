using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class PlayerControllerBPatches
    {
        private static ManualLogSource Logger { get; set; }

        public static void Init(ManualLogSource logger)
        {
            Logger = logger;
        }

        [HarmonyPatch(typeof(PlayerControllerB), "DamagePlayerClientRpc")]
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        static void DamagePlayerPatch(PlayerControllerB __instance, int damageNumber)
        {
            if (__instance.playerClientId != 0)
                return;

            var damage = (float)damageNumber;
            Logger.LogInfo($"DamagePlayer got called: {damage} ({damage / 100f})");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateDamageReceivedEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(damage / 100f);
            }
        }

        [HarmonyPatch(typeof(PlayerControllerB), "KillPlayerClientRpc")]
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        static void KillPlayerPatch(PlayerControllerB __instance)
        {
            if (__instance.playerClientId != 0)
                return;

            Logger.LogInfo($"KillPlayer got called");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateKilledEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(1.0f);
            }
        }
    }
}
