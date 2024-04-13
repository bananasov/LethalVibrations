using GameNetcodeStuff;
using LethalVibrations.Buttplug;
using LethalVibrations.Utils;

namespace LethalVibrations.Hooks;

public class EnemyAIHooks
{
    [PatchInit]
    public static void Init()
    {
        LethalVibrations.Logger.LogInfo("Patching EnemyAI functions.");

        On.EnemyAI.HitEnemy += EnemyAIOnHitEnemy;
    }

    private static void EnemyAIOnHitEnemy(On.EnemyAI.orig_HitEnemy orig, EnemyAI self, int force,
        PlayerControllerB playerwhohit, bool playhitsfx, int hitid)
    {
        orig(self, force, playerwhohit, playhitsfx, hitid);

        if (LethalVibrations.DeviceManager.IsConnected() && Config.Damage.Dealt.Enabled!.Value)
        {
            LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(Config.Damage.Dealt.Strength!.Value,
                Config.Damage.Dealt.Duration!.Value);
        }
    }
}