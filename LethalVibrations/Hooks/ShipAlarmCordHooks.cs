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

        On.ShipAlarmCord.PullCordClientRpc += ShipAlarmCordOnPullCordClientRpc;
        On.ShipAlarmCord.StopPullingCordClientRpc += ShipAlarmCordOnStopPullingCordClientRpc;
        // Maybe i have to use transpilers for this? probably not but who knows lol
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