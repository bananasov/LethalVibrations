using GameNetcodeStuff;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class EnemyAIPatches
    {
        [HarmonyPatch(typeof(EnemyAI), "HitEnemy")]
        [HarmonyPostfix]
        private static void HitEnemyPatch(int force, PlayerControllerB playerWhoHit)
        {
            if (playerWhoHit.playerClientId != GameNetworkManager.Instance.localPlayerController.playerClientId)
                return;

            Plugin.Mls.LogDebug($"HitEnemy got called");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateDamageDealtEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevicesWithDuration(Config.VibrateDamageDealtStrength.Value, Config.VibrateDamageDealtDuration.Value);
            }
        }
    }
}
