using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buttplug.Client;
using Buttplug.Core;
using UnityEngine;

namespace LethalHaptics.Buttplug;

public class DeviceManager
{
    private List<ButtplugClientDevice> ConnectedDevices { get; set; }
    private ButtplugClient ButtplugClient { get; set; }
    private string ServerUri { get; set; }


    public DeviceManager(string clientName, string serverUri)
    {
        ConnectedDevices = new List<ButtplugClientDevice>();
        ButtplugClient = new ButtplugClient(clientName);
        ServerUri = serverUri;

        Plugin.Log.LogInfo("BP client created for " + clientName);
        ButtplugClient.DeviceAdded += HandleDeviceAdded;
        ButtplugClient.DeviceRemoved += HandleDeviceRemoved;
    }

    public async void ConnectDevices()
    {
        if (ButtplugClient.Connected) return;

        try
        {
            Plugin.Log.LogInfo($"Attempting to connect to Intiface server at {ServerUri}");
            await ButtplugClient.ConnectAsync(new ButtplugWebsocketConnector(new Uri(ServerUri)));
            Plugin.Log.LogInfo("Connection successful. Beginning scan for devices");
            await ButtplugClient.StartScanningAsync();
        }
        catch (ButtplugException exception)
        {
            Plugin.Log.LogError(
                $"Attempt to connect to devices failed. Ensure Intiface is running and attempt to reconnect from the 'Devices' section in the mod's in-game settings.");
            Plugin.Log.LogDebug($"ButtplugIO error occured while connecting devices: {exception}");
        }
    }

    public async void Disconnect()
    {
        StopConnectedDevices();
        await ButtplugClient.DisconnectAsync();
    }

    public void VibrateConnectedDevices(double intensity)
    {
        ConnectedDevices.ForEach(Action);
        return;

        async void Action(ButtplugClientDevice device)
        {
            await device.VibrateAsync(Mathf.Clamp((float)intensity, 0f, 1.0f));
        }
    }

    public void VibrateConnectedDevicesWithDuration(float intensity, float time)
    {
        ConnectedDevices.ForEach(Action);
        return;

        async void Action(ButtplugClientDevice device)
        {
            await device.VibrateAsync(Mathf.Clamp((float)intensity, 0f, 1.0f));
            await Task.Delay((int)(time * 1000f));
            await device.VibrateAsync(0.0f);
        }
    }

    public void StopConnectedDevices()
    {
        ConnectedDevices.ForEach(device => device.Stop());
    }

    public bool IsConnected() => ButtplugClient.Connected;

    private void HandleDeviceAdded(object sender, DeviceAddedEventArgs args)
    {
        if (!IsVibratableDevice(args.Device))
        {
            Plugin.Log.LogInfo($"{args.Device.Name} was detected but ignored due to it not being vibratable.");
            return;
        }

        Plugin.Log.LogInfo($"{args.Device.Name} connected to client {ButtplugClient.Name}");
        ConnectedDevices.Add(args.Device);
    }

    private void HandleDeviceRemoved(object sender, DeviceRemovedEventArgs args)
    {
        if (!IsVibratableDevice(args.Device))
        {
            return;
        }

        Plugin.Log.LogInfo($"{args.Device.Name} disconnected from client {ButtplugClient.Name}");
        ConnectedDevices.Remove(args.Device);
    }

    private static bool IsVibratableDevice(ButtplugClientDevice device)
    {
        return device.VibrateAttributes.Count > 0;
    }
}