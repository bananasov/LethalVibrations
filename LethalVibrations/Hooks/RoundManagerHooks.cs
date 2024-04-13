using LethalVibrations.Buttplug;
using LethalVibrations.Utils;

namespace LethalVibrations.Hooks;

public class RoundManagerHooks
{
    [PatchInit]
    public static void Init()
    {
        LethalVibrations.Logger.LogInfo("Patching RoundManager functions.");

        On.RoundManager.DespawnPropsAtEndOfRound += RoundManagerOnDespawnPropsAtEndOfRound;
    }

    private static void RoundManagerOnDespawnPropsAtEndOfRound(On.RoundManager.orig_DespawnPropsAtEndOfRound orig,
        RoundManager self, bool despawnAllItems)
    {
        orig(self, despawnAllItems);

        if (LethalVibrations.DeviceManager.IsConnected() && Config.RoundSurvival.Enabled!.Value &&
            !GameNetworkManager.Instance.localPlayerController.isPlayerDead)
        {
            LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(Config.RoundSurvival.Strength!.Value,
                Config.RoundSurvival.Duration!.Value);
        }
    }
}