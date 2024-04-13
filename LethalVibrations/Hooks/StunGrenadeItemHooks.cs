using GameNetcodeStuff;
using LethalVibrations.Buttplug;
using LethalVibrations.Utils;
using UnityEngine;

namespace LethalVibrations.Hooks;

public class StunGrenadeItemHooks
{
    [PatchInit]
    public static void Init()
    {
        LethalVibrations.Logger.LogInfo("Patching StunGrenadeItem functions.");

        On.StunGrenadeItem.StunExplosion += StunGrenadeItemOnStunExplosion;
    }

    private static void StunGrenadeItemOnStunExplosion(On.StunGrenadeItem.orig_StunExplosion orig,
        Vector3 explosionPosition, bool affectAudio, float flashSeverityMultiplier, float enemyStunTime,
        float flashSeverityDistanceRolloff, bool isHeldItem, PlayerControllerB playerHeldBy,
        PlayerControllerB playerThrownBy, float addToFlashSeverity)
    {
        orig(explosionPosition, affectAudio, flashSeverityMultiplier, enemyStunTime, flashSeverityDistanceRolloff,
            isHeldItem, playerHeldBy, playerThrownBy, addToFlashSeverity);

        if (LethalVibrations.DeviceManager.IsConnected() && Config.Flashbang.Enabled!.Value)
        {
            LethalVibrations.DeviceManager.VibrateConnectedDevicesWithDuration(Config.Flashbang.Strength!.Value,
                Config.Flashbang.Duration!.Value);
        }
    }
}