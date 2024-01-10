using BepInEx;
using BepInEx.Configuration;

namespace LethalVibrations.Buttplug
{
    internal class Config
    {
        private static ConfigFile ConfigFile { get; set; }

        internal static ConfigEntry<string> ServerUri { get; set; }

        internal static ConfigEntry<float> VibrateAmplifier { get; set; }
        internal static ConfigEntry<bool> Rewarding { get; set; }

        #region Damage recieved config entries
        internal static ConfigEntry<bool> VibrateDamageReceivedEnabled { get; set; }
        internal static ConfigEntry<float> VibrateDamageReceivedDuration { get; set; }
        internal static ConfigEntry<float> VibrateDamageReceivedAmplifier { get; set; }
        #endregion

        #region Damage Dealt config entries
        internal static ConfigEntry<bool> VibrateDamageDealtEnabled { get; set; }
        internal static ConfigEntry<float> VibrateDamageDealtDuration { get; set; }
        internal static ConfigEntry<float> VibrateDamageDealtStrength { get; set; }
        #endregion

        #region Player death config entries
        internal static ConfigEntry<bool> VibrateKilledEnabled { get; set; }
        internal static ConfigEntry<float> VibrateKilledDuration { get; set; }
        internal static ConfigEntry<float> VibrateKilledStrength { get; set; }
        #endregion

        #region Charge item vibration config entries
        internal static ConfigEntry<bool> VibrateItemChargerChargeEnabled { get; set; }
        internal static ConfigEntry<float> VibrateItemChargerChargeDuration { get; set; }
        internal static ConfigEntry<float> VibrateItemChargerChargeStrength { get; set; }
        #endregion

        #region Screen shake vibration config entries
        internal static ConfigEntry<bool> VibrateScreenShakeEnabled { get; set; }
        internal static ConfigEntry<float> VibrateScreenShakeDuration { get; set; }
        internal static ConfigEntry<float> VibrateScreenShakeAmplifier { get; set; }
        #endregion

        #region Scan ping vibration config entries
        internal static ConfigEntry<bool> PingScanEnabled { get; set; }
        internal static ConfigEntry<float> PingScanDuration { get; set; }
        internal static ConfigEntry<float> PingScanStrength { get; set; }

        #endregion

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
            Rewarding = ConfigFile.Bind("Vibrations", "GoodBoyMode", false, "Enable rewarding");
            
            VibrateDamageReceivedEnabled = ConfigFile.Bind("Vibrations.DamageReceived", "Enabled", true, "Vibrate when you receive damage");
            VibrateDamageReceivedDuration = ConfigFile.Bind("Vibrations.DamageReceived", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateDamageReceivedAmplifier = ConfigFile.Bind("Vibrations.DamageReceived", "Amplifier", 0.0f, "Change the amplification of vibration");
            
            VibrateDamageDealtEnabled = ConfigFile.Bind("Vibrations.DamageDealt", "Enabled", true, "Vibrate when you deal damage");
            VibrateDamageDealtDuration = ConfigFile.Bind("Vibrations.DamageDealt", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateDamageDealtStrength = ConfigFile.Bind("Vibrations.DamageDealt", "Strength", 0.5f, "Change the strength of vibration");
            
            VibrateKilledEnabled = ConfigFile.Bind("Vibrations.PlayerKilled", "Enabled", true, "Vibrate when you die");
            VibrateKilledDuration = ConfigFile.Bind("Vibrations.PlayerKilled", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateKilledStrength = ConfigFile.Bind("Vibrations.PlayerKilled", "Strength", 1.0f, "Change the strength of vibration");
            
            VibrateItemChargerChargeEnabled = ConfigFile.Bind("Vibrations.ItemCharge", "Enabled", true, "Vibrate when you charge an item");
            VibrateItemChargerChargeDuration = ConfigFile.Bind("Vibrations.ItemCharge", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateItemChargerChargeStrength = ConfigFile.Bind("Vibrations.ItemCharge", "Strength", 0.4f, "Change the strength of vibration");

            VibrateScreenShakeEnabled = ConfigFile.Bind("Vibrations.ShakeScreen", "Enabled", true, "Vibrate when your screen shakes");
            VibrateScreenShakeDuration = ConfigFile.Bind("Vibrations.ShakeScreen", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateScreenShakeAmplifier = ConfigFile.Bind("Vibrations.ShakeScreen", "Amplifier", 0.0f, "Change the amplification of vibration");

            PingScanEnabled = ConfigFile.Bind("Vibrations.PingScan", "Enabled", false, "Vibrate when you press right click");
            PingScanDuration = ConfigFile.Bind("Vibrations.PingScan", "Duration", 0.3f, "Length of time to vibrate for");
            PingScanStrength = ConfigFile.Bind("Vibrations.PingScan", "Strength", 0.3f, "Change the strength of vibration");
        }
    }
}
