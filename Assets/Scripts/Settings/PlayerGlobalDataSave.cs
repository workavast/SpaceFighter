using System;
using Settings.Coins;
using Settings.Localization;
using Settings.Missions;
using Settings.Spaceship;
using Settings.Volume;
using Settings.Weapons;

namespace Settings
{
    [Serializable]
    public class PlayerGlobalDataSave
    {
        public VolumeSettingsSave volumeSettingsSave;
        public MissionsSettingsSave missionsSettingsSave;
        public CoinsSettingsSave coinsSettingsSave;
        public SpaceshipSettingsSave spaceshipSettingsSave;
        public WeaponsSettingsSave weaponsSettingsSave;
        public LocalizationSettingsSave localizationSettingsSave;

        public PlayerGlobalDataSave()
        {
            volumeSettingsSave = new();
            missionsSettingsSave = new();
            coinsSettingsSave = new();
            spaceshipSettingsSave = new();
            weaponsSettingsSave = new();
            localizationSettingsSave = new();
        }
        
        public PlayerGlobalDataSave(
            VolumeSettings volumeSettings,
            MissionsSettings missionsSettings,
            CoinsSettings coinsSettings,
            SpaceshipSettings spaceshipSettings,
            WeaponsSettings weaponsSettings,
            LocalizationSettings localizationSettings)
        {
            volumeSettingsSave = new VolumeSettingsSave(volumeSettings);
            missionsSettingsSave = new MissionsSettingsSave(missionsSettings);
            coinsSettingsSave = new CoinsSettingsSave(coinsSettings);
            spaceshipSettingsSave = new SpaceshipSettingsSave(spaceshipSettings);
            weaponsSettingsSave = new WeaponsSettingsSave(weaponsSettings);
            localizationSettingsSave = new LocalizationSettingsSave(localizationSettings);
        }
    }
}