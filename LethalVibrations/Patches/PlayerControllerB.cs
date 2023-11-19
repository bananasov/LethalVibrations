using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class PlayerControllerBPatches
    {
        internal static ManualLogSource Logger { get; set; }

        public static void Init(ManualLogSource logger)
        {
            Logger = logger;
        }

        [HarmonyPatch(typeof(PlayerControllerB), "DamagePlayerClientRpc")]
        [HarmonyPostfix]
        static void DamagePlayerPatch(int damageNumber)
        {
            var damage = (float)damageNumber;
            Logger.LogInfo($"DamagePlayer got called: {damage} ({damage / 100f})");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateDamageRecieved.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(damage / 100f);
            }
        }

        [HarmonyPatch(typeof(PlayerControllerB), "KillPlayerClientRpc")]
        [HarmonyPostfix]
        static void KillPlayerPatch(PlayerControllerB __instance)
        {
            if (__instance.playerClientId != 0) { return; }
            Logger.LogInfo($"KillPlayer got called");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateKilled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(1.0f);
            }
        }
    }
}
