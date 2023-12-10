using BepInEx;
using BepInEx.Configuration;

namespace LethalVibrations.Buttplug
{
    internal class Config
    {
        private static ConfigFile ConfigFile { get; set; }

        internal static ConfigEntry<string> ServerUri { get; set; }

        internal static ConfigEntry<float> VibrateAmplifier { get; set; }
        internal static ConfigEntry<bool> GoodboyMode { get; set; }

        #region Damage recieved config entries
        internal static ConfigEntry<bool> VibrateDamageReceivedEnabled { get; set; }
        internal static ConfigEntry<float> VibrateDamageReceivedDuration { get; set; }
        internal static ConfigEntry<float> VibrateDamageReceivedAmplifier { get; set; }
        #endregion

        #region Damage Dealt config entries
        internal static ConfigEntry<bool> VibrateDamageDealtEnabled { get; set; }
        internal static ConfigEntry<float> VibrateDamageDealtDuration { get; set; }
        internal static ConfigEntry<float> VibrateDamageDealtAmplifier { get; set; }
        #endregion

        #region Player death config entries
        internal static ConfigEntry<bool> VibrateKilledEnabled { get; set; }
        internal static ConfigEntry<float> VibrateKilledDuration { get; set; }
        internal static ConfigEntry<float> VibrateKilledAmplifier { get; set; }
        #endregion

        #region Charge item vibration config entries
        internal static ConfigEntry<bool> VibrateItemChargerChargeEnabled { get; set; }
        internal static ConfigEntry<float> VibrateItemChargerChargeDuration { get; set; }
        internal static ConfigEntry<float> VibrateItemChargerChargeAmplifier { get; set; }
        #endregion
        
        #region Walkie talkie vibration config entries
        internal static ConfigEntry<bool> VibrateWalkieTalkieReceivedEnabled { get; set; }
        internal static ConfigEntry<float> VibrateWalkieTalkieReceivedDuration { get; set; }
        internal static ConfigEntry<float> VibrateWalkieTalkieReceivedAmplifier { get; set; }
        #endregion

        #region Shake screen vibration config entries
        internal static ConfigEntry<bool> VibrateShakeScreenEnabled { get; set; }
        internal static ConfigEntry<float> VibrateShakeScreenDuration { get; set; }
        internal static ConfigEntry<float> VibrateShakeScreenAmplifier { get; set; }
        #endregion

        #region Scan ping vibration config entries
        internal static ConfigEntry<bool> PingScanEnabled { get; set; }
        internal static ConfigEntry<float> PingScanDuration { get; set; }
        internal static ConfigEntry<float> PingScanAmplifier { get; set; }

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
            GoodboyMode = ConfigFile.Bind("Vibrations", "GoodBoyMode", false, "Enabled rewarding");
            
            VibrateDamageReceivedEnabled = ConfigFile.Bind("Vibrations.DamageReceived", "Enabled", true, "Vibrate when you receive damage");
            VibrateDamageReceivedDuration = ConfigFile.Bind("Vibrations.DamageReceived", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateDamageReceivedAmplifier = ConfigFile.Bind("Vibrations.DamageReceived", "Amplifier", 0.0f, "Change the amplification of vibration");
            
            VibrateDamageDealtEnabled = ConfigFile.Bind("Vibrations.DamageDealt", "Enabled", true, "Vibrate when you deal damage");
            VibrateDamageDealtDuration = ConfigFile.Bind("Vibrations.DamageDealt", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateDamageDealtAmplifier = ConfigFile.Bind("Vibrations.DamageDealt", "Amplifier", 0.0f, "Change the amplification of vibration");
            
            VibrateKilledEnabled = ConfigFile.Bind("Vibrations.PlayerKilled", "Enabled", true, "Vibrate when you die");
            VibrateKilledDuration = ConfigFile.Bind("Vibrations.PlayerKilled", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateKilledAmplifier = ConfigFile.Bind("Vibrations.PlayerKilled", "Amplifier", 0.0f, "Change the amplification of vibration");
            
            VibrateItemChargerChargeEnabled = ConfigFile.Bind("Vibrations.ItemCharge", "Enabled", true, "Vibrate when you charge an item");
            VibrateItemChargerChargeDuration = ConfigFile.Bind("Vibrations.ItemCharge", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateItemChargerChargeAmplifier = ConfigFile.Bind("Vibrations.ItemCharge", "Amplifier", 0.0f, "Change the amplification of vibration");
            
            VibrateWalkieTalkieReceivedEnabled = ConfigFile.Bind("Vibrations.WalkieTalkieReceived", "Enabled", false, "Vibrate when you receive audio from the WalkieTalkie");
            VibrateWalkieTalkieReceivedDuration = ConfigFile.Bind("Vibrations.WalkieTalkieReceived", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateWalkieTalkieReceivedAmplifier = ConfigFile.Bind("Vibrations.WalkieTalkieReceived", "Amplifier", 0.0f, "Change the amplification of vibration");

            VibrateShakeScreenEnabled = ConfigFile.Bind("Vibrations.ShakeScreen", "Enabled", true, "Vibrate when your screen shakes");
            VibrateShakeScreenDuration = ConfigFile.Bind("Vibrations.ShakeScreen", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateShakeScreenAmplifier = ConfigFile.Bind("Vibrations.ShakeScreen", "Amplifier", 0.0f, "Change the amplification of vibration");

            PingScanEnabled = ConfigFile.Bind("Vibrations.PingScan", "Enabled", true, "Vibrate when you press right click");
            PingScanDuration = ConfigFile.Bind("Vibrations.PingScan", "Duration", 0.3f, "Length of time to vibrate for");
            PingScanAmplifier = ConfigFile.Bind("Vibrations.PingScan", "Amplifier", 0.0f, "Change the amplification of vibration");
        }
    }
}
