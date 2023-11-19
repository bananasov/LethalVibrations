using BepInEx;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LethalVibrations.Buttplug
{
    internal class Config
    {
        private static ConfigFile ConfigFile { get; set; }

        internal static ConfigEntry<string> ServerUri { get; set; }

        internal static ConfigEntry<float> VibrateAmplifier { get; set; }
        
        internal static ConfigEntry<bool> VibrateDamageReceivedEnabled { get; set; }
        internal static ConfigEntry<int> VibrateDamageReceivedTime { get; set; }
        internal static ConfigEntry<float> VibrateDamageReceivedAmplifier { get; set; }
        
        internal static ConfigEntry<bool> VibrateDamageDealtEnabled { get; set; }
        internal static ConfigEntry<int> VibrateDamageDealtTime { get; set; }
        internal static ConfigEntry<float> VibrateDamageDealtAmplifier { get; set; }
        
        internal static ConfigEntry<bool> VibrateKilledEnabled { get; set; }
        internal static ConfigEntry<int> VibrateKilledTime { get; set; }
        internal static ConfigEntry<float> VibrateKilledAmplifier { get; set; }

        internal static ConfigEntry<bool> VibrateWalkieTalkieReceivedEnabled { get; set; }
        internal static ConfigEntry<int> VibrateWalkieTalkieReceivedTime { get; set; }
        internal static ConfigEntry<float> VibrateWalkieTalkieReceivedAmplifier { get; set; }

        internal static ConfigEntry<bool> VibrateItemChargerChargeEnabled { get; set; }
        internal static ConfigEntry<int> VibrateItemChargerChargeTime { get; set; }
        internal static ConfigEntry<float> VibrateItemChargerChargeAmplifier { get; set; }


        static Config()
        {
            ConfigFile = new ConfigFile(Paths.ConfigPath + "\\LethalVibrations.cfg", true);

            ServerUri = ConfigFile.Bind(
                "Devices",
                "Server Uri",
                "ws://localhost:12345",
                "URI of the Intiface server."
            );

            VibrateAmplifier =
                ConfigFile.Bind("Vibrations", "Amplifier", 0.0f, "Change the amplification of vibration");
            
            VibrateDamageReceivedEnabled = ConfigFile.Bind("Vibrations.DamageReceived", "Enabled", true, "Vibrate when you receive damage");
            VibrateDamageReceivedTime = ConfigFile.Bind("Vibrations.DamageReceived", "Time", 1, "Length of time to vibrate for");
            VibrateDamageReceivedAmplifier = ConfigFile.Bind("Vibrations.DamageReceived", "Amplifier", 0.0f, "Change the amplification of vibration");
            
            VibrateDamageDealtEnabled = ConfigFile.Bind("Vibrations.DamageDealt", "Enabled", true, "Vibrate when you deal damage");
            VibrateDamageDealtTime = ConfigFile.Bind("Vibrations.DamageDealt", "Time", 1, "Length of time to vibrate for");
            VibrateDamageDealtAmplifier = ConfigFile.Bind("Vibrations.DamageDealt", "Amplifier", 0.0f, "Change the amplification of vibration");
            
            VibrateKilledEnabled = ConfigFile.Bind("Vibrations.PlayerKilled", "Enabled", true, "Vibrate when you die");
            VibrateKilledTime = ConfigFile.Bind("Vibrations.PlayerKilled", "Time", 1, "Length of time to vibrate for");
            VibrateKilledAmplifier = ConfigFile.Bind("Vibrations.PlayerKilled", "Amplifier", 0.0f, "Change the amplification of vibration");
            
            VibrateItemChargerChargeEnabled = ConfigFile.Bind("Vibrations.ItemCharge", "Enabled", true, "Vibrate when you charge an item");
            VibrateItemChargerChargeTime = ConfigFile.Bind("Vibrations.ItemCharge", "Time", 1, "Length of time to vibrate for");
            VibrateItemChargerChargeAmplifier = ConfigFile.Bind("Vibrations.ItemCharge", "Amplifier", 0.0f, "Change the amplification of vibration");
            
            VibrateWalkieTalkieReceivedEnabled = ConfigFile.Bind("Vibrations.WalkieTalkieReceived", "Enabled", true, "Vibrate when you receive audio from the WalkieTalkie");
            VibrateWalkieTalkieReceivedTime = ConfigFile.Bind("Vibrations.WalkieTalkieReceived", "Time", 1, "Length of time to vibrate for");
            VibrateWalkieTalkieReceivedAmplifier = ConfigFile.Bind("Vibrations.WalkieTalkieReceived", "Amplifier", 0.0f, "Change the amplification of vibration");
        }
    }
}
