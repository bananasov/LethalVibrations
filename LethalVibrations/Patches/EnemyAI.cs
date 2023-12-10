using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class EnemyAIPatches
    {
        private static ManualLogSource Logger { get; set; }

        public static void Init(ManualLogSource logger)
        {
            Logger = logger;
        }

        [HarmonyPatch(typeof(EnemyAI), "HitEnemy")]
        [HarmonyPostfix]
        static void HitEnemyPatch(int force, PlayerControllerB playerWhoHit)
        {
            if (playerWhoHit.playerClientId != GameNetworkManager.Instance.localPlayerController.playerClientId)
                return;

            Logger.LogInfo($"HitEnemy got called");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateDamageDealtEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(0.5f + Config.VibrateDamageDealtAmplifier.Value, Config.VibrateDamageDealtDuration.Value);
            }
        }

    }
}
