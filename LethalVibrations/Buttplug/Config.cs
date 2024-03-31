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

        #region Quota Reached config entries
        internal static ConfigEntry<bool> QuotaReachedEnabled { get; set; }
        internal static ConfigEntry<float> QuotaReachedDuration { get; set; }
        internal static ConfigEntry<float> QuotaReachedStrength { get; set; }
        #endregion
        
        #region Round Survival config entries
        internal static ConfigEntry<bool> RoundSurvivalEnabled { get; set; }
        internal static ConfigEntry<float> RoundSurvivalDuration { get; set; }
        internal static ConfigEntry<float> RoundSurvivalStrength { get; set; }
        #endregion
        
        #region Airhorn config entries
        internal static ConfigEntry<bool> AirhornEnabled { get; set; }
        internal static ConfigEntry<float> AirhornDuration { get; set; }
        internal static ConfigEntry<float> AirhornStrength { get; set; }
        #endregion
        
        #region Ship Horn config entries
        internal static ConfigEntry<bool> ShipHornEnabled { get; set; }
        internal static ConfigEntry<float> ShipHornStrength { get; set; }
        #endregion

        #region Flashbang/Stun Grenade config entries
        internal static ConfigEntry<bool> FlashbangEnabled { get; set; }
        internal static ConfigEntry<float> FlashbangStrength { get; set; }
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

            #region Rewarding stuff
            Rewarding = ConfigFile.Bind("Vibrations", "Rewarding", true, "Enable rewarding");
            
            VibrateDamageDealtEnabled = ConfigFile.Bind("Vibrations.DamageDealt", "Enabled", true, "Vibrate when you deal damage");
            VibrateDamageDealtDuration = ConfigFile.Bind("Vibrations.DamageDealt", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateDamageDealtStrength = ConfigFile.Bind("Vibrations.DamageDealt", "Strength", 0.5f, "Change the strength of vibration");
            
            QuotaReachedEnabled = ConfigFile.Bind("Vibrations.QuotaReached", "Enabled", true, "Vibrate when you reach the quota");
            QuotaReachedDuration = ConfigFile.Bind("Vibrations.QuotaReached", "Duration", 1.0f, "Length of time to vibrate for");
            QuotaReachedStrength = ConfigFile.Bind("Vibrations.QuotaReached", "Strength", 0.5f, "Change the strength of vibration");
            
            RoundSurvivalEnabled = ConfigFile.Bind("Vibrations.RoundSurvival", "Enabled", true, "Vibrate when you survive the round");
            RoundSurvivalDuration = ConfigFile.Bind("Vibrations.RoundSurvival", "Duration", 1.0f, "Length of time to vibrate for");
            RoundSurvivalStrength = ConfigFile.Bind("Vibrations.RoundSurvival", "Strength", 0.3f, "Change the strength of vibration");
            #endregion

            #region Punishing stuff  
            VibrateDamageReceivedEnabled = ConfigFile.Bind("Vibrations.DamageReceived", "Enabled", true, "Vibrate when you receive damage");
            VibrateDamageReceivedDuration = ConfigFile.Bind("Vibrations.DamageReceived", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateDamageReceivedAmplifier = ConfigFile.Bind("Vibrations.DamageReceived", "Amplifier", 0.0f, "Change the amplification of vibration");
            
            VibrateKilledEnabled = ConfigFile.Bind("Vibrations.PlayerKilled", "Enabled", false, "Vibrate when you die");
            VibrateKilledDuration = ConfigFile.Bind("Vibrations.PlayerKilled", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateKilledStrength = ConfigFile.Bind("Vibrations.PlayerKilled", "Strength", 1.0f, "Change the strength of vibration");

            PingScanEnabled = ConfigFile.Bind("Vibrations.PingScan", "Enabled", false, "Vibrate when you press right click");
            PingScanDuration = ConfigFile.Bind("Vibrations.PingScan", "Duration", 0.3f, "Length of time to vibrate for");
            PingScanStrength = ConfigFile.Bind("Vibrations.PingScan", "Strength", 0.3f, "Change the strength of vibration");
            #endregion
            
            VibrateItemChargerChargeEnabled = ConfigFile.Bind("Vibrations.ItemCharge", "Enabled", true, "Vibrate when you charge an item");
            VibrateItemChargerChargeDuration = ConfigFile.Bind("Vibrations.ItemCharge", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateItemChargerChargeStrength = ConfigFile.Bind("Vibrations.ItemCharge", "Strength", 0.4f, "Change the strength of vibration");

            VibrateScreenShakeEnabled = ConfigFile.Bind("Vibrations.ShakeScreen", "Enabled", true, "Vibrate when your screen shakes");
            VibrateScreenShakeDuration = ConfigFile.Bind("Vibrations.ShakeScreen", "Duration", 1.0f, "Length of time to vibrate for");
            VibrateScreenShakeAmplifier = ConfigFile.Bind("Vibrations.ShakeScreen", "Amplifier", 0.0f, "Change the amplification of vibration");
            
            AirhornEnabled = ConfigFile.Bind("Vibrations.Airhorn", "Enabled", false, "Vibrate when someone uses the airhorn");
            AirhornDuration = ConfigFile.Bind("Vibrations.Airhorn", "Duration", 1.0f, "Length of time to vibrate for");
            AirhornStrength = ConfigFile.Bind("Vibrations.Airhorn", "Strength", 0.1f, "Change the intensity of vibration");
            
            ShipHornEnabled = ConfigFile.Bind("Vibrations.ShipHorn", "Enabled", true, "Vibrate when someone airs the ships horn");
            ShipHornStrength = ConfigFile.Bind("Vibrations.ShipHorn", "Strength", 0.5f, "Change the intensity of vibration");
            
            FlashbangEnabled = ConfigFile.Bind("Vibrations.Flashbang", "Enabled", true, "Vibrate when a flashbang goes off");
            FlashbangStrength = ConfigFile.Bind("Vibrations.Flashbang", "Strength", 0.5f, "Change the intensity of vibration");
        }
    }
}
