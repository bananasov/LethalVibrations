﻿using BepInEx.Logging;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    internal class GrabbableObjectPatches
    {
        private static ManualLogSource Logger { get; set; }

        public static void Init(ManualLogSource logger)
        {
            Logger = logger;
        }

        [HarmonyPatch(typeof(GrabbableObject), "GrabItemOnClient")]
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        static void GrabItemOnClientPatch(GrabbableObject __instance)
        {
            if (!__instance.itemProperties.isScrap)
                return;

            Logger.LogInfo("GrabItemOnClient called");
            if (Plugin.DeviceManager.IsConnected() && Config.Rewarding.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(0.2f + Config.VibrateKilledAmplifier.Value, Config.VibrateKilledDuration.Value);
            }
        }
    }
}
