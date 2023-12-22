using BepInEx.Logging;
using Buttplug.Client;
using Buttplug.Client.Connectors.WebsocketConnector;
using Buttplug.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

// Some code was stolen from https://github.com/quasikyo/rumble-rain/blob/main/RumbleRain/DeviceManager.cs
namespace LethalVibrations.Buttplug
{
    public class DeviceManager
    {
        private List<ButtplugClientDevice> ConnectedDevices { get; set; }
        private ButtplugClient ButtplugClient { get; set; }

        public DeviceManager(string clientName)
        {
            ConnectedDevices = new List<ButtplugClientDevice>();
            ButtplugClient = new ButtplugClient(clientName);
            Plugin.Mls.LogInfo("BP client created for " + clientName);
            ButtplugClient.DeviceAdded += HandleDeviceAdded;
            ButtplugClient.DeviceRemoved += HandleDeviceRemoved;
        }

        public bool IsConnected() => ButtplugClient.Connected;

        public async void ConnectDevices()
        {
            if (ButtplugClient.Connected) { return; }

            try
            {
                Plugin.Mls.LogInfo($"Attempting to connect to Intiface server at {Config.ServerUri.Value}");
                await ButtplugClient.ConnectAsync(new ButtplugWebsocketConnector(new Uri(Config.ServerUri.Value)));
                Plugin.Mls.LogInfo("Connection successful. Beginning scan for devices");
                await ButtplugClient.StartScanningAsync();
            }
            catch (ButtplugException exception)
            {
                Plugin.Mls.LogError($"Attempt to connect to devices failed. Ensure Intiface is running and attempt to reconnect from the 'Devices' section in the mod's in-game settings.");
                Plugin.Mls.LogDebug($"ButtplugIO error occured while connecting devices: {exception}");
            }
        }

        public void VibrateConnectedDevices(double intensity, float time)
        {
            intensity += Config.VibrateAmplifier.Value;

            async void Action(ButtplugClientDevice device)
            {
                await device.VibrateAsync(Mathf.Clamp((float)intensity, 0f, 1.0f));
                await Task.Delay((int)(time * 1000f));
                await device.VibrateAsync(0.0f);
            }

            ConnectedDevices.ForEach(Action);
        }

        public void StopConnectedDevices()
        {
            ConnectedDevices.ForEach(async (ButtplugClientDevice device) => await device.Stop());
        }


        internal void CleanUp()
        {
            StopConnectedDevices();
        }

        private void HandleDeviceAdded(object sender, DeviceAddedEventArgs args)
        {
            if (!IsVibratableDevice(args.Device))
            {
                Plugin.Mls.LogInfo($"{args.Device.Name} was detected but ignored due to it not being vibratable.");
                return;
            }

            Plugin.Mls.LogInfo($"{args.Device.Name} connected to client {ButtplugClient.Name}");
            ConnectedDevices.Add(args.Device);
        }

        private void HandleDeviceRemoved(object sender, DeviceRemovedEventArgs args)
        {
            if (!IsVibratableDevice(args.Device)) { return; }

            Plugin.Mls.LogInfo($"{args.Device.Name} disconnected from client {ButtplugClient.Name}");
            ConnectedDevices.Remove(args.Device);
        }


        private bool IsVibratableDevice(ButtplugClientDevice device)
        {
            return device.VibrateAttributes.Count > 0;
        }
    }
}
