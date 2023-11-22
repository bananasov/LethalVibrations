using BepInEx.Logging;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class EnemyAI
    {
        private static ManualLogSource Logger { get; set; }

        public static void Init(ManualLogSource logger)
        {
            Logger = logger;
        }

        [HarmonyPatch(typeof(EnemyAI), "HitEnemyOnLocalClient")]
        [HarmonyPostfix]
        static void HitEnemyOnLocalClientPatch()
        {
            Logger.LogInfo($"HitEnemyOnLocalClient got called");

            if (Plugin.DeviceManager.IsConnected() && Config.VibrateDamageDealtEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(0.5f + Config.VibrateDamageDealtAmplifier.Value, Config.VibrateDamageDealtTime.Value);
            }
        }
    }
}
