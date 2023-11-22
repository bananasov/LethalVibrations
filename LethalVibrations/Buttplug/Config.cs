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
        internal static ConfigEntry<float> VibrateDamageReceivedTime { get; set; }
        internal static ConfigEntry<float> VibrateDamageReceivedAmplifier { get; set; }
        #endregion

        #region Damage dealt config entries
        internal static ConfigEntry<bool> VibrateDamageDealtEnabled { get; set; }
        internal static ConfigEntry<float> VibrateDamageDealtTime { get; set; }
        internal static ConfigEntry<float> VibrateDamageDealtAmplifier { get; set; }
        #endregion

        #region Player death config entries
        internal static ConfigEntry<bool> VibrateKilledEnabled { get; set; }
        internal static ConfigEntry<float> VibrateKilledTime { get; set; }
        internal static ConfigEntry<float> VibrateKilledAmplifier { get; set; }
        #endregion

        #region Charge item vibration config entries
        internal static ConfigEntry<bool> VibrateItemChargerChargeEnabled { get; set; }
        internal static ConfigEntry<float> VibrateItemChargerChargeTime { get; set; }
        internal static ConfigEntry<float> VibrateItemChargerChargeAmplifier { get; set; }
        #endregion
        
        #region Walkie talkie vibration config entries
        internal static ConfigEntry<bool> VibrateWalkieTalkieReceivedEnabled { get; set; }
        internal static ConfigEntry<float> VibrateWalkieTalkieReceivedTime { get; set; }
        internal static ConfigEntry<float> VibrateWalkieTalkieReceivedAmplifier { get; set; }
        #endregion

        #region Shake screen vibration config entries
        internal static ConfigEntry<bool> VibrateShakeScreenEnabled { get; set; }
        internal static ConfigEntry<float> VibrateShakeScreenTime { get; set; }
        internal static ConfigEntry<float> VibrateShakeScreenAmplifier { get; set; }
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
            GoodboyMode = ConfigFile.Bind("Vibrations", "GoodBoyMode", false, "Rewards you instead of punishing you");
            
            #region Damage recieved binds
            VibrateDamageReceivedEnabled = ConfigFile.Bind("Vibrations.DamageReceived", "Enabled", true, "Vibrate when you receive damage");
            VibrateDamageReceivedTime = ConfigFile.Bind("Vibrations.DamageReceived", "Time", 1.0f, "Length of time to vibrate for");
            VibrateDamageReceivedAmplifier = ConfigFile.Bind("Vibrations.DamageReceived", "Amplifier", 0.0f, "Change the amplification of vibration");
            #endregion
            
            #region Damage delt binds
            VibrateDamageDealtEnabled = ConfigFile.Bind("Vibrations.DamageDealt", "Enabled", true, "Vibrate when you deal damage");
            VibrateDamageDealtTime = ConfigFile.Bind("Vibrations.DamageDealt", "Time", 1.0f, "Length of time to vibrate for");
            VibrateDamageDealtAmplifier = ConfigFile.Bind("Vibrations.DamageDealt", "Amplifier", 0.0f, "Change the amplification of vibration");
            #endregion
            
            #region Player death binds
            VibrateKilledEnabled = ConfigFile.Bind("Vibrations.PlayerKilled", "Enabled", true, "Vibrate when you die");
            VibrateKilledTime = ConfigFile.Bind("Vibrations.PlayerKilled", "Time", 1.0f, "Length of time to vibrate for");
            VibrateKilledAmplifier = ConfigFile.Bind("Vibrations.PlayerKilled", "Amplifier", 0.0f, "Change the amplification of vibration");
            #endregion
            
            #region Charge item binds
            VibrateItemChargerChargeEnabled = ConfigFile.Bind("Vibrations.ItemCharge", "Enabled", true, "Vibrate when you charge an item");
            VibrateItemChargerChargeTime = ConfigFile.Bind("Vibrations.ItemCharge", "Time", 1.0f, "Length of time to vibrate for");
            VibrateItemChargerChargeAmplifier = ConfigFile.Bind("Vibrations.ItemCharge", "Amplifier", 0.0f, "Change the amplification of vibration");
            #endregion
            
            #region Walkie talkie binds
            VibrateWalkieTalkieReceivedEnabled = ConfigFile.Bind("Vibrations.WalkieTalkieReceived", "Enabled", true, "Vibrate when you receive audio from the WalkieTalkie");
            VibrateWalkieTalkieReceivedTime = ConfigFile.Bind("Vibrations.WalkieTalkieReceived", "Time", 1.0f, "Length of time to vibrate for");
            VibrateWalkieTalkieReceivedAmplifier = ConfigFile.Bind("Vibrations.WalkieTalkieReceived", "Amplifier", 0.0f, "Change the amplification of vibration");
            #endregion
            
            #region Shake screen binds
            VibrateShakeScreenEnabled = ConfigFile.Bind("Vibrations.ShakeScreen", "Enabled", true, "Vibrate when your screen shakes");
            VibrateShakeScreenTime = ConfigFile.Bind("Vibrations.ShakeScreen", "Time", 1.0f, "Length of time to vibrate for");
            VibrateShakeScreenAmplifier = ConfigFile.Bind("Vibrations.ShakeScreen", "Amplifier", 0.0f, "Change the amplification of vibration");
            #endregion
        }
    }
}
