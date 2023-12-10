using BepInEx.Logging;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches;

internal class WalkieTalkiePatches
{
    private static ManualLogSource Logger { get; set; }

    public static void Init(ManualLogSource logger)
    {
        Logger = logger;
    }

    [HarmonyPatch(typeof(WalkieTalkie), "SendWalkieTalkieStartTransmissionSFX")]
    [HarmonyPostfix]
    private static void SendWalkieTalkieStartTransmissionSfxPatch(int playerId)
    {
        if (playerId == (int)GameNetworkManager.Instance.localPlayerController.playerClientId)
            return;

        Logger.LogDebug($"SendWalkieTalkieStartTransmissionSFX got called");

        if (Plugin.DeviceManager.IsConnected() && Config.VibrateWalkieTalkieReceivedEnabled.Value)
        {
            Plugin.DeviceManager.VibrateConnectedDevices(0.6f + Config.VibrateWalkieTalkieReceivedAmplifier.Value, Config.VibrateWalkieTalkieReceivedDuration.Value);
        }
    }
}