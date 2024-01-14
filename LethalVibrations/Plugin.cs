using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static DeviceManager DeviceManager { get; private set; }
        internal static ManualLogSource Mls { get; private set; }
        
        private void Awake()
        {
            Mls = Logger;
            
            DeviceManager = new DeviceManager("LethalVibrations");
            DeviceManager.ConnectDevices();

            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll(typeof(Patches.PlayerControllerBPatches));
            harmony.PatchAll(typeof(Patches.ItemChargerPatches));
            harmony.PatchAll(typeof(Patches.EnemyAIPatches));
            harmony.PatchAll(typeof(Patches.HUDManagerPatches));
            harmony.PatchAll(typeof(Patches.GrabbableObjectPatches));
            harmony.PatchAll(typeof(Patches.RoundManagerPatches));
            harmony.PatchAll(typeof(Patches.NoisemakerPropPatches));

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} ({PluginInfo.PLUGIN_VERSION}) is loaded!");
        }
    }
}
