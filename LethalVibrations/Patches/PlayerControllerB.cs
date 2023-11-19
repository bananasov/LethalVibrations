using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;

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

            if (Plugin.DeviceManager.IsConnected())
            {
                // TODO: Fix this.
                Plugin.DeviceManager.VibrateConnectedDevices(damage / 100f);
            }
        }

        [HarmonyPatch(typeof(PlayerControllerB), "KillPlayerClientRpc")]
        [HarmonyPostfix]
        static void KillPlayerPatch()
        {
            Logger.LogInfo($"KillPlayer got called");

            if (Plugin.DeviceManager.IsConnected())
            {
                Plugin.DeviceManager.VibrateConnectedDevices(1.0f);
            }
        }
    }
}
