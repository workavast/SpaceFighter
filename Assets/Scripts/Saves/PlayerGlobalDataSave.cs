using System;
using Saves.Coins;
using Saves.Localization;
using Saves.Missions;
using Saves.Spaceship;
using Saves.Volume;
using Saves.Weapons;

namespace Saves
{
    [Serializable]
    public class PlayerGlobalDataSave
    {
        public VolumeSettingsSave VolumeSettingsSave;
        public MissionsSettingsSave missionsSettingsSave;
        public CoinsSettingsSave coinsSettingsSave;
        public SpaceshipSettingsSave spaceshipSettingsSave;
        public WeaponsSettingsSave weaponsSettingsesSave;
        public LocalizationSettingsSave localizationSettingsSave;
        
        public PlayerGlobalDataSave(
            VolumeSettings volumeSettings,
            MissionsSettings missionsSettings,
            CoinsSettings coinsSettings,
            SpaceshipSettings spaceshipSettings,
            WeaponsSettings weaponsSettings,
            LocalizationSettings localizationSettings)
        {
            VolumeSettingsSave = new VolumeSettingsSave(volumeSettings);
            missionsSettingsSave = new MissionsSettingsSave(missionsSettings);
            coinsSettingsSave = new CoinsSettingsSave(coinsSettings);
            spaceshipSettingsSave = new SpaceshipSettingsSave(spaceshipSettings);
            weaponsSettingsesSave = new WeaponsSettingsSave(weaponsSettings);
            localizationSettingsSave = new LocalizationSettingsSave(localizationSettings);
        }
    }
}