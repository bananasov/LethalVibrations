using BepInEx;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LethalVibrations.Buttplug
{
    internal class Config
    {
        internal static ConfigFile ConfigFile { get; set; }

        internal static ConfigEntry<string> ServerUri { get; set; }

        internal static ConfigEntry<float> VibrateAmplifier { get; set; }
        internal static ConfigEntry<bool> VibrateDamageRecieved { get; set; }
        internal static ConfigEntry<bool> VibrateDamageDealt { get; set; }
        internal static ConfigEntry<bool> VibrateKilled { get; set; }
        internal static ConfigEntry<bool> VibrateWalkieTalkieRecieved { get; set; }
        internal static ConfigEntry<bool> VibrateItemChargerCharge { get; set; }

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
            VibrateDamageRecieved = ConfigFile.Bind("Vibrations", "VibrateRecieved", true, "Vibrate when you recieve damage");
            VibrateDamageDealt = ConfigFile.Bind("Vibrations", "VibrateDealt", true, "Vibrate when you deal damage");
            VibrateKilled = ConfigFile.Bind("Vibrations", "VibrateKilled", true, "Vibrate when you die");
            VibrateItemChargerCharge = ConfigFile.Bind("Vibrations", "ItemCharge", true, "Vibrate when you charge an item");
            VibrateWalkieTalkieRecieved = ConfigFile.Bind("Vibrations", "VibrateWalkieTalkieRecieved", true, "Vibrate when you recieve audio from the WalkieTalkie");
        }
    }
}
