using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    public class RoundManagerPatches
    {
        [HarmonyPatch(typeof(RoundManager), "DespawnPropsAtEndOfRound")]
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        private static void GrabItemOnClientPatch(RoundManager __instance)
        {
            Plugin.Mls.LogDebug("DespawnPropsAtEndOfRound called");

            if (Plugin.DeviceManager.IsConnected() && Config.RoundSurvivalEnabled.Value && !GameNetworkManager.Instance.localPlayerController.isPlayerDead)
            {
                Plugin.DeviceManager.VibrateConnectedDevicesWithDuration(Config.RoundSurvivalStrength.Value, Config.RoundSurvivalDuration.Value);
            }
        }
    }
}

