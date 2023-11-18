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

        private static ConfigEntry<string> serverUri { get; set; }

        internal static ConfigEntry<bool> vibrateDamageRecieved { get; set; }
        internal static ConfigEntry<bool> vibrateDamageDealt { get; set; }

        static Config()
        {
            ConfigFile = new ConfigFile(Paths.ConfigPath + "\\LethalVibrations.cfg", true);

            serverUri = ConfigFile.Bind(
                "Devices",
                "Server Uri",
                "ws://localhost:12345",
                "URI of the Intiface server."
            );

            vibrateDamageRecieved = ConfigFile.Bind("Vibrations", "VibrateRecieved", true, "Vibrate when you recieve damage");
            vibrateDamageDealt = ConfigFile.Bind("Vibrations", "VibrateDealt", true, "Vibrate when you deal damage");
        }
    }
}
