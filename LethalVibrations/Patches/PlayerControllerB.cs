using BepInEx.Logging;
using HarmonyLib;

namespace LethalVibrations.Patches
{
    internal class PlayerControllerB
    {
        internal static ManualLogSource Logger { get; set; }

        public static void Init(ManualLogSource logger)
        {
            Logger = logger;
        }

        [HarmonyPatch(typeof(GameNetcodeStuff.PlayerControllerB), "DamagePlayer")]
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

        [HarmonyPatch(typeof(GameNetcodeStuff.PlayerControllerB), "KillPlayer")]
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
