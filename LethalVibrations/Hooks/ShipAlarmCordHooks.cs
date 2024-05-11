using LethalVibrations.Buttplug;
using LethalVibrations.Utils;
using MonoMod.Cil;

namespace LethalVibrations.Hooks;

public class ShipAlarmCordHooks
{
    [PatchInit]
    public static void Init()
    {
        LethalVibrations.Logger.LogInfo("Patching ShipAlarmCord functions.");

        // FIXME: this doesn't seem to work.
        // On.ShipAlarmCord.PullCordClientRpc += ShipAlarmCordOnPullCordClientRpc;
        // On.ShipAlarmCord.StopPullingCordClientRpc += ShipAlarmCordOnStopPullingCordClientRpc;
    }

    private static void ShipAlarmCordOnStopPullingCordClientRpc(On.ShipAlarmCord.orig_StopPullingCordClientRpc orig,
        ShipAlarmCord self, int playerPullingCord)
    {
        orig(self, playerPullingCord);

        if (LethalVibrations.DeviceManager.IsConnected() && Config.ShipHorn.Enabled!.Value)
        {
            LethalVibrations.DeviceManager.VibrateConnectedDevices(Config.ShipHorn.Strength!.Value);
        }
    }

    private static void ShipAlarmCordOnPullCordClientRpc(On.ShipAlarmCord.orig_PullCordClientRpc orig,
        ShipAlarmCord self, int playerPullingCord)
    {
        orig(self, playerPullingCord);

        if (LethalVibrations.DeviceManager.IsConnected() && Config.ShipHorn.Enabled!.Value)
        {
            LethalVibrations.DeviceManager.StopConnectedDevices();
        }
    }
}