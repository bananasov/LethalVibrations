using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        // Fuck BepInEx
        private const string PLUGIN_GUID = "github.bananasov.LethalVibrations";
        private const string PLUGIN_NAME = "LethalVibrations";
        private const string PLUGIN_VERSION = "0.0.5";
        
        internal static DeviceManager DeviceManager { get; private set; }
        internal static ManualLogSource Mls { get; private set; }
        
        private void Awake()
        {
            Mls = Logger;
            
            DeviceManager = new DeviceManager("LethalVibrations");
            DeviceManager.ConnectDevices();

            var harmony = new Harmony(PLUGIN_GUID);
            harmony.PatchAll(typeof(Patches.PlayerControllerBPatches));
            harmony.PatchAll(typeof(Patches.ItemChargerPatches));
            harmony.PatchAll(typeof(Patches.EnemyAIPatches));
            harmony.PatchAll(typeof(Patches.HUDManagerPatches));
            harmony.PatchAll(typeof(Patches.GrabbableObjectPatches));

            Logger.LogInfo($"Plugin {PLUGIN_NAME} ({PLUGIN_VERSION}) is loaded!");
        }
    }
}
