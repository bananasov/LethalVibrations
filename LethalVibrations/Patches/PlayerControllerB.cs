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

        [HarmonyPatch(typeof(PlayerControllerB), "DamagePlayer")]
        [HarmonyPostfix]
        static void DamagePlayerPatch(int damageNumber)
        {
            Logger.LogInfo($"DamagePlayer got called: {damageNumber} ({damageNumber / 100})");

            if (Plugin.DeviceManager.IsConnected())
            {
                // TODO: Fix this.
                Plugin.DeviceManager.VibrateConnectedDevices(damageNumber / 100);
            }
        }

        [HarmonyPatch(typeof(PlayerControllerB), "KillPlayer")]
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
