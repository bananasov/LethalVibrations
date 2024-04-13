using Buttplug.Client;
using Buttplug.Client.Connectors.WebsocketConnector;
using Buttplug.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

// Some code was stolen from https://github.com/quasikyo/rumble-rain/blob/main/RumbleRain/DeviceManager.cs
namespace LethalVibrations.Buttplug;

public class DeviceManager
{
    private List<ButtplugClientDevice> ConnectedDevices { get; }
    private ButtplugClient ButtplugClient { get; }
    public event EventHandler<VibratedEventArgs> OnVibrated;

    public DeviceManager(string clientName)
    {
        ConnectedDevices = new List<ButtplugClientDevice>();
        ButtplugClient = new ButtplugClient(clientName);
        LethalVibrations.Logger.LogInfo("BP client created for " + clientName);
        ButtplugClient.DeviceAdded += HandleDeviceAdded;
        ButtplugClient.DeviceRemoved += HandleDeviceRemoved;
    }

    public bool IsConnected() => ButtplugClient.Connected;

    public async void ConnectDevices()
    {
        if (ButtplugClient.Connected) return;

        try
        {
            LethalVibrations.Logger.LogInfo($"Attempting to connect to Intiface server at {Config.ServerUri.Value}");
            await ButtplugClient.ConnectAsync(new ButtplugWebsocketConnector(new Uri(Config.ServerUri.Value)));
            LethalVibrations.Logger.LogInfo("Connection successful. Beginning scan for devices");
            await ButtplugClient.StartScanningAsync();
        }
        catch (ButtplugException exception)
        {
            LethalVibrations.Logger.LogError(
                $"Attempt to connect to devices failed. Ensure Intiface is running and attempt to reconnect from the 'Devices' section in the mod's in-game settings.");
            LethalVibrations.Logger.LogDebug($"ButtplugIO error occured while connecting devices: {exception}");
        }
    }

    public void VibrateConnectedDevicesWithDuration(float intensity, float time)
    {
        ConnectedDevices.ForEach(Action);
        OnVibrated(this, new VibratedEventArgs(time, intensity));
        return;

        async void Action(ButtplugClientDevice device)
        {
            await device.VibrateAsync(Mathf.Clamp((float)intensity, 0f, 1.0f));
            await Task.Delay((int)(time * 1000f));
            await device.VibrateAsync(0.0f);
        }
    }

    /// <summary>
    ///  This has to be manually stopped
    /// </summary>
    public void VibrateConnectedDevices(double intensity)
    {
        ConnectedDevices.ForEach(Action);
        return;

        async void Action(ButtplugClientDevice device)
        {
            await device.VibrateAsync(Mathf.Clamp((float)intensity, 0f, 1.0f));
        }
    }

    public void StopConnectedDevices()
    {
        ConnectedDevices.ForEach(async device => await device.Stop());
    }

    internal void CleanUp()
    {
        StopConnectedDevices();
    }

    private void HandleDeviceAdded(object sender, DeviceAddedEventArgs args)
    {
        if (!IsVibratableDevice(args.Device))
        {
            LethalVibrations.Logger.LogInfo($"{args.Device.Name} was detected but ignored due to it not being vibratable.");
            return;
        }

        LethalVibrations.Logger.LogInfo($"{args.Device.Name} connected to client {ButtplugClient.Name}");
        ConnectedDevices.Add(args.Device);
    }

    private void HandleDeviceRemoved(object sender, DeviceRemovedEventArgs args)
    {
        if (!IsVibratableDevice(args.Device))
        {
            return;
        }

        LethalVibrations.Logger.LogInfo($"{args.Device.Name} disconnected from client {ButtplugClient.Name}");
        ConnectedDevices.Remove(args.Device);
    }

    private static bool IsVibratableDevice(ButtplugClientDevice device)
    {
        return device.VibrateAttributes.Count > 0;
    }
}

public class VibratedEventArgs
{
    public float Duration { get; set; }
    public float Strength { get; set; }

    public VibratedEventArgs(float duration, float strength)
    {
        Duration = duration;
        Strength = strength;
    }
}