using BepInEx.Logging;
using Buttplug.Client;
using Buttplug.Client.Connectors.WebsocketConnector;
using Buttplug.Core;
using System;
using System.Collections.Generic;

// Some code was stolen from https://github.com/quasikyo/rumble-rain/blob/main/RumbleRain/DeviceManager.cs
namespace LethalVibrations.Buttplug
{
    internal class DeviceManager
    {
        private enum DeviceState
        {
            /// <summary>
            /// Open to input and devices are vibrating.
            /// </summary>
            Active,
            /// <summary>
            /// Open to input and devices are not vibrating.
            /// </summary>
            Inactive,
            /// <summary>
            /// Closed to input and devices are not vibrating.
            /// </summary>
            Paused
        }

        private DeviceState _state;
        private DeviceState State
        {
            get => _state;
            set
            {
                Log.LogDebug($"State updated from {_state} to {value}");
                _state = value;
            }
        }

        private static ManualLogSource Log { get; set; }

        private List<ButtplugClientDevice> ConnectedDevices { get; set; }
        private ButtplugClient ButtplugClient { get; set; }

        public DeviceManager(ManualLogSource logger, string clientName)
        {
            Log = logger;
            State = DeviceState.Inactive;

            ConnectedDevices = new List<ButtplugClientDevice>();
            ButtplugClient = new ButtplugClient(clientName);
            Log.LogInfo("BP client created for " + clientName);
            ButtplugClient.DeviceAdded += HandleDeviceAdded;
            ButtplugClient.DeviceRemoved += HandleDeviceRemoved;
        }

        public async void ConnectDevices()
        {
            if (ButtplugClient.Connected) { return; }

            try
            {
                Log.LogInfo($"Attempting to connect to Intiface server at {Config.serverUri.Value}");
                await ButtplugClient.ConnectAsync(new ButtplugWebsocketConnector(new Uri(Config.serverUri.Value)));
                Log.LogInfo("Connection successful. Beginning scan for devices");
                await ButtplugClient.StartScanningAsync();
            }
            catch (ButtplugException exception)
            {
                Log.LogError($"Attempt to connect to devices failed. Ensure Intiface is running and attempt to reconnect from the 'Devices' section in the mod's in-game settings.");
                Log.LogDebug($"ButtplugIO error occured while connecting devices: {exception}");
            }
        }

        private void VibrateConnectedDevices(double intensity)
        {
            State = DeviceState.Active;

            ConnectedDevices.ForEach(async (ButtplugClientDevice device) => {
                await device.VibrateAsync(intensity);
            });
        }

        private void StopConnectedDevices(DeviceState newState = DeviceState.Inactive)
        {
            if (newState == DeviceState.Active)
            {
                throw new ArgumentException($"{nameof(newState)}={newState} is invalid. Expecting {DeviceState.Inactive} or {DeviceState.Active}.");
            }

            State = newState;
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
                Log.LogInfo($"{args.Device.Name} was detected but ignored due to it not being vibratable.");
                return;
            }

            Log.LogInfo($"{args.Device.Name} connected to client {ButtplugClient.Name}");
            ConnectedDevices.Add(args.Device);
        }

        private void HandleDeviceRemoved(object sender, DeviceRemovedEventArgs args)
        {
            if (!IsVibratableDevice(args.Device)) { return; }

            Log.LogInfo($"{args.Device.Name} disconnected from client {ButtplugClient.Name}");
            ConnectedDevices.Remove(args.Device);
        }


        private bool IsVibratableDevice(ButtplugClientDevice device)
        {
            return device.VibrateAttributes.Count > 0;
        }
    }
}
