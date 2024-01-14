using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations.Patches
{
    public class ShipAlarmCordPatches
    {
        [HarmonyPatch(typeof(ShipAlarmCord), "PullCordClientRpc")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> PullCordClientRpcTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var newInstructions = new List<CodeInstruction>(instructions);

            var index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Nop) + 1;
            
            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ShipAlarmCordPatches), nameof(ShipAlarmCordPatches.StartVibrating))),
            });

            foreach (var t in newInstructions)
                yield return t;
        }
        
        [HarmonyPatch(typeof(ShipAlarmCord), "StopPullingCordClientRpc")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> StopPullingCordClientRpcTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var newInstructions = new List<CodeInstruction>(instructions);

            var index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Nop) + 1;
            
            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(ShipAlarmCordPatches), nameof(ShipAlarmCordPatches.StopVibrating))),
            });

            foreach (var t in newInstructions)
                yield return t;
        }

        #region This part makes the vibrations work on the client
        [HarmonyPatch(typeof(ShipAlarmCord), "HoldCordDown")]
        [HarmonyPostfix]
        private static void HoldCordDownPatch()
        {
            Plugin.Mls.LogDebug($"ShipAlarmCord.HoldCordDown got called");
            
            // I am well aware that `HoldCordDown` gets called SO MANY FUCKING TIMES.
            StartVibrating();
        }
        
        [HarmonyPatch(typeof(ShipAlarmCord), "StopHorn")]
        [HarmonyPostfix]
        private static void StopHornPatch()
        {
            Plugin.Mls.LogDebug($"ShipAlarmCord.StopHorn got called");
            
            StopVibrating();
        }
        #endregion

        #region Helper functions so the IL transpiler can call stuff :3
        private static void StartVibrating()
        {
            if (Plugin.DeviceManager.IsConnected() && Config.ShipHornEnabled.Value)
            {
                Plugin.DeviceManager.VibrateConnectedDevices(Config.ShipHornStrength.Value);
            }
        }
        
        private static void StopVibrating()
        {
            if (Plugin.DeviceManager.IsConnected() && Config.ShipHornEnabled.Value)
            {
                Plugin.DeviceManager.StopConnectedDevices();
            }
        }
        #endregion
    }
}