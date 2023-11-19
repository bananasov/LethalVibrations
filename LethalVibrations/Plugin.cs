using BepInEx;
using HarmonyLib;
using LethalVibrations.Buttplug;

namespace LethalVibrations
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static DeviceManager DeviceManager { get; private set; }

        private void Awake()
        {
            DeviceManager = new DeviceManager(Logger, "LethalVibrations");
            DeviceManager.ConnectDevices();

            Patches.PlayerControllerB.Init(Logger);

            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll(typeof(Patches.PlayerControllerB));

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} ({PluginInfo.PLUGIN_VERSION}) is loaded!");
        }
    }
}
