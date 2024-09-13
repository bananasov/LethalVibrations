using BepInEx;
using BepInEx.Logging;
using LethalHaptics.Buttplug;

[BepInPlugin(LCMPluginInfo.PLUGIN_GUID, LCMPluginInfo.PLUGIN_NAME, LCMPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
  public static ManualLogSource Log = null!;
  public static DeviceManager DeviceManager = null!;

  private void Awake()
  {
    Log = Logger;
    DeviceManager = new DeviceManager("LethalHaptics", "ws://127.0.0.1:12345");

    Log.LogInfo($"Plugin {LCMPluginInfo.PLUGIN_NAME} is loaded!");
  }

}
