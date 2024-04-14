using System;
using BepInEx;
using BepInEx.Logging;
using LethalVibrations.Buttplug;
using LethalVibrations.Utils;

namespace LethalVibrations;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class LethalVibrations : BaseUnityPlugin
{
    public static LethalVibrations Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;
    internal static DeviceManager DeviceManager { get; private set; } = null!;

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;
        
        DeviceManager = new DeviceManager("LethalVibrations");
        DeviceManager.ConnectDevices();

        Hook();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    private static void Hook()
    {
        var methodsWithAttribute = ReflectionUtility.GetMethodsWithAttribute<PatchInitAttribute>();
        foreach (var valueTuple in methodsWithAttribute)
        {
            var method = valueTuple.Item1;
            method.Invoke(null, Array.Empty<object>()); 
        }

        Logger.LogDebug("Finished Hooking!");
    }
}
