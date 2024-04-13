using BepInEx;
using BepInEx.Configuration;

namespace LethalVibrations.Buttplug;

internal static class Config
{
    private static ConfigFile ConfigFile { get; set; }

    internal static ConfigEntry<string> ServerUri { get; set; }

    internal static class Damage
    {
        /// <summary>
        /// Vibration config entries for when you take damage
        /// </summary>
        internal static class Taken
        {
            internal static ConfigEntry<bool>? Enabled { get; set; }
            internal static ConfigEntry<float>? Duration { get; set; }
        }

        /// <summary>
        /// Vibration config entries for when you deal damage
        /// </summary>
        internal static class Dealt
        {
            internal static ConfigEntry<bool>? Enabled { get; set; }
            internal static ConfigEntry<float>? Duration { get; set; }
            internal static ConfigEntry<float>? Strength { get; set; }
        }
    }

    /// <summary>
    /// Vibration config entries for when you die
    /// </summary>
    internal static class Death
    {
        internal static ConfigEntry<bool>? Enabled { get; set; }
        internal static ConfigEntry<float>? Duration { get; set; }
        internal static ConfigEntry<float>? Strength { get; set; }
    }

    /// <summary>
    /// Vibration config entries for charging items 
    /// </summary>
    internal static class ItemCharge
    {
        internal static ConfigEntry<bool>? Enabled { get; set; }
        internal static ConfigEntry<float>? Duration { get; set; }
        internal static ConfigEntry<float>? Strength { get; set; }
    }

    /// <summary>
    /// Vibration config entries for when you pick up scrap
    /// </summary>
    internal static class ScrapPickup
    {
        internal static ConfigEntry<bool>? Enabled { get; set; }
        internal static ConfigEntry<float>? Duration { get; set; }
        internal static ConfigEntry<float>? Strength { get; set; }
    }

    /// <summary>
    /// Vibration config entries for screen shake
    /// </summary>
    internal static class ScreenShake
    {
        internal static ConfigEntry<bool>? Enabled { get; set; }
    }

    /// <summary>
    /// Vibration config entries for scanning (the thing you constantly do with your right mouse button)
    /// </summary>
    internal static class Scanning
    {
        internal static ConfigEntry<bool>? Enabled { get; set; }
        internal static ConfigEntry<float>? Duration { get; set; }
        internal static ConfigEntry<float>? Strength { get; set; }
    }

    /// <summary>
    /// Vibration config entries for when you reach your quota
    /// </summary>
    internal static class QuotaReached
    {
        internal static ConfigEntry<bool>? Enabled { get; set; }
        internal static ConfigEntry<float>? Duration { get; set; }
        internal static ConfigEntry<float>? Strength { get; set; }
    }

    /// <summary>
    /// Vibration config entries for when you survive a round
    /// </summary>
    internal static class RoundSurvival
    {
        internal static ConfigEntry<bool>? Enabled { get; set; }
        internal static ConfigEntry<float>? Duration { get; set; }
        internal static ConfigEntry<float>? Strength { get; set; }
    }

    /// <summary>
    /// Vibration config entries for when you use/uses (or anyone for that matter) the airhorn
    /// </summary>
    internal static class Airhorn
    {
        internal static ConfigEntry<bool>? Enabled { get; set; }
        internal static ConfigEntry<float>? Duration { get; set; }
        internal static ConfigEntry<float>? Strength { get; set; }
    }

    /// <summary>
    /// Vibration config entries for when you use/uses (or anyone for that matter) the ship horn
    /// </summary>
    internal static class ShipHorn
    {
        internal static ConfigEntry<bool>? Enabled { get; set; }
        internal static ConfigEntry<float>? Strength { get; set; }
    }

    /// <summary>
    /// Vibration config entries for when you use/uses (or anyone for that matter) the flashbang
    /// </summary>
    internal static class Flashbang
    {
        internal static ConfigEntry<bool>? Enabled { get; set; }
        internal static ConfigEntry<float>? Duration { get; set; }
        internal static ConfigEntry<float>? Strength { get; set; }
    }

    static Config()
    {
        ConfigFile = new ConfigFile(Paths.ConfigPath + "\\LethalVibrations.cfg", true);

        ServerUri = ConfigFile.Bind("Devices", "Server Uri", "ws://localhost:12345", "URI of the Intiface server.");

        #region Damage related stuff

        Damage.Taken.Enabled =
            ConfigFile.Bind("Vibrations.Damage.Taken", "Enabled", true, "Vibrate when you take damage");
        Damage.Taken.Duration = ConfigFile.Bind("Vibrations.Damage.Taken", "Duration", 0.1f,
            "How long to vibrate when you take damage");

        Damage.Dealt.Enabled =
            ConfigFile.Bind("Vibrations.Damage.Dealt", "Enabled", true, "Vibrate when you deal damage");
        Damage.Dealt.Duration = ConfigFile.Bind("Vibrations.Damage.Dealt", "Duration", 0.1f,
            "How long to vibrate when you deal damage");
        Damage.Dealt.Strength = ConfigFile.Bind("Vibrations.Damage.Dealt", "Strength", 0.3f,
            "How strong to vibrate when you deal damage");

        #endregion

        #region Death related stuff

        Death.Enabled = ConfigFile.Bind("Vibrations.Death", "Enabled", true, "Vibrate when you die");
        Death.Duration = ConfigFile.Bind("Vibrations.Death", "Duration", 1.0f, "How long to vibrate when you die");
        Death.Strength = ConfigFile.Bind("Vibrations.Death", "Strength", 1.0f, "How strong to vibrate when you die");

        #endregion

        #region Scrap pickup related stuff

        ScrapPickup.Enabled =
            ConfigFile.Bind("Vibrations.ScrapPickup", "Enabled", true, "Vibrate when you pick up scrap");
        ScrapPickup.Duration = ConfigFile.Bind("Vibrations.ScrapPickup", "Duration", 0.1f,
            "How long to vibrate when you pick up scrap");
        ScrapPickup.Strength = ConfigFile.Bind("Vibrations.ScrapPickup", "Strength", 0.3f,
            "How strong to vibrate when you pick up scrap");

        #endregion

        #region Item charge related stuff

        ItemCharge.Enabled =
            ConfigFile.Bind("Vibrations.ItemCharge", "Enabled", true, "Vibrate when you charge an item");
        ItemCharge.Duration = ConfigFile.Bind("Vibrations.ItemCharge", "Duration", 1.0f,
            "How long to vibrate when you charge an item");
        ItemCharge.Strength = ConfigFile.Bind("Vibrations.ItemCharge", "Strength", 1.0f,
            "How strong to vibrate when you charge an item");

        #endregion

        #region Screen shake related stuff

        ScreenShake.Enabled =
            ConfigFile.Bind("Vibrations.ScreenShake", "Enabled", true, "Vibrate when you shake the screen");

        #endregion

        #region Scanning related stuff

        Scanning.Enabled = ConfigFile.Bind("Vibrations.Scanning", "Enabled", true, "Vibrate when you scan");
        Scanning.Duration =
            ConfigFile.Bind("Vibrations.Scanning", "Duration", 0.1f, "How long to vibrate when you scan");
        Scanning.Strength =
            ConfigFile.Bind("Vibrations.Scanning", "Strength", 0.2f, "How strong to vibrate when you scan");

        #endregion

        #region Quota reached related stuff

        QuotaReached.Enabled =
            ConfigFile.Bind("Vibrations.QuotaReached", "Enabled", true, "Vibrate when you reach quota");
        QuotaReached.Duration = ConfigFile.Bind("Vibrations.QuotaReached", "Duration", 1.0f,
            "How long to vibrate when you reach quota");
        QuotaReached.Strength = ConfigFile.Bind("Vibrations.QuotaReached", "Strength", 1.0f,
            "How strong to vibrate when you reach quota");

        #endregion

        #region Round survival related stuff

        RoundSurvival.Enabled =
            ConfigFile.Bind("Vibrations.RoundSurvival", "Enabled", true, "Vibrate when you survive a round");
        RoundSurvival.Duration = ConfigFile.Bind("Vibrations.RoundSurvival", "Duration", 0.5f,
            "How long to vibrate when you survive a round");
        RoundSurvival.Strength = ConfigFile.Bind("Vibrations.RoundSurvival", "Strength", 1.0f,
            "How strong to vibrate when you survive a round");

        #endregion

        #region Airhorn related stuff

        Airhorn.Enabled = ConfigFile.Bind("Vibrations.Airhorn", "Enabled", true, "Vibrate when you use an airhorn");
        Airhorn.Duration = ConfigFile.Bind("Vibrations.Airhorn", "Duration", 0.1f,
            "How long to vibrate when you use an airhorn");
        Airhorn.Strength = ConfigFile.Bind("Vibrations.Airhorn", "Strength", 0.2f,
            "How strong to vibrate when you use an airhorn");

        #endregion

        #region Ship horn related stuff

        ShipHorn.Enabled = ConfigFile.Bind("Vibrations.ShipHorn", "Enabled", true, "Vibrate when you use a ship horn");
        ShipHorn.Strength = ConfigFile.Bind("Vibrations.ShipHorn", "Strength", 0.6f,
            "How strong to vibrate when you use a ship horn");

        #endregion

        #region Flashbang related stuff

        Flashbang.Enabled =
            ConfigFile.Bind("Vibrations.Flashbang", "Enabled", true, "Vibrate when you use a flashbang");
        Flashbang.Duration = ConfigFile.Bind("Vibrations.Flashbang", "Duration", 0.1f,
            "How long to vibrate when you use a flashbang");
        Flashbang.Strength = ConfigFile.Bind("Vibrations.Flashbang", "Strength", 0.5f,
            "How strong to vibrate when you use a flashbang");

        #endregion
    }
}