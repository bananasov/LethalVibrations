using LethalVibrations.Buttplug;
using LethalVibrations.Utils;
using On.GameNetcodeStuff;
using UnityEngine;

namespace LethalVibrations.Hooks;

public class PlayerControllerBHooks
{
    [PatchInit]
    public static void Init()
    {
        LethalVibrations.Logger.LogInfo("Patching PlayerControllerB functions.");

        PlayerControllerB.DamagePlayer += PlayerControllerBOnDamagePlayer;
        PlayerControllerB.KillPlayer += PlayerControllerBOnKillPlayer;
    }

    private static void PlayerControllerBOnKillPlayer(PlayerControllerB.orig_KillPlayer orig,
        GameNetcodeStuff.PlayerControllerB self, Vector3 bodyVelocity, bool spawnBody, CauseOfDeath causeOfDeath,
        int deathAnimation)
    {
        orig(self, bodyVelocity, spawnBody, causeOfDeath, deathAnimation);

        if (LethalVibrations.DeviceManager.IsConnected() && Config.Death.Enabled!.Value)
        {
            LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(Config.Death.Strength!.Value,
                Config.Death.Duration!.Value);
        }
    }

    private static void PlayerControllerBOnDamagePlayer(PlayerControllerB.orig_DamagePlayer orig,
        GameNetcodeStuff.PlayerControllerB self, int damageNumber, bool hasDamageSfx, bool callRpc,
        CauseOfDeath causeOfDeath, int deathAnimation, bool fallDamage, Vector3 force)
    {
        orig(self, damageNumber, hasDamageSfx, callRpc, causeOfDeath, deathAnimation, fallDamage, force);

        if (!LethalVibrations.DeviceManager.IsConnected() || !Config.Damage.Taken.Enabled!.Value) return;

        var damage = (float)damageNumber / 100;

        LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(damage,
            Config.Damage.Taken.Duration!.Value);
    }
}