using BepInEx;
using BepInEx.Logging;
using LethalHaptics.Buttplug;
using LethalHaptics.Utils;

[BepInPlugin(LCMPluginInfo.PLUGIN_GUID, LCMPluginInfo.PLUGIN_NAME, LCMPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    public static ManualLogSource Log = null!;
    public static DeviceManager DeviceManager = null!;

    private void Awake()
    {
        Log = Logger;

        // TODO: Make serverUri configurable
        DeviceManager = new DeviceManager("LethalHaptics", "ws://127.0.0.1:12345");
        DeviceManager.ConnectDevices();
        
        Hook();

        Log.LogInfo($"Plugin {LCMPluginInfo.PLUGIN_NAME} is loaded!");
    }

    private static void Hook()
    {
        var methodsWithAttribute = Reflection.GetMethodsWithAttribute<ButtplugPatchAttribute>();
        foreach (var valueTuple in methodsWithAttribute)
        {
            var method = valueTuple.Item1;
            method.Invoke(null, []);
        }

        Log.LogDebug("Finished Hooking!");
    }
}