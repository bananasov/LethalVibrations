using GameNetcodeStuff;
using LethalHaptics.Utils;

namespace LethalHaptics.Hooks;

// ReSharper disable once UnusedType.Global
public class EnemyAIHooks : IHook
{
    [ButtplugPatch]
    public void Initialize()
    {
        On.EnemyAI.HitEnemy += EnemyAIOnHitEnemy;
    }

    private static void EnemyAIOnHitEnemy(On.EnemyAI.orig_HitEnemy orig, EnemyAI self, int force, PlayerControllerB playerWhoHit, bool playHitSfx, int hitID)
    {
        orig(self, force, playerWhoHit, playHitSfx, hitID);

        if (Plugin.DeviceManager.IsConnected())
        {
            Plugin.Log.LogDebug("Yarr hit enemy");
        }
    }
}