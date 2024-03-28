using Initializers;
using Saves.Coins;
using Saves.Localization;
using Saves.Missions;
using Saves.Savers;
using Saves.Spaceship;
using Saves.Volume;
using Saves.Weapons;

namespace Saves
{
    public class PlayerGlobalData
    {
        private const string SaveKey = "PlayerGlobalData";
        
        private static PlayerGlobalData _instance;
        public static PlayerGlobalData Instance => _instance ??= new PlayerGlobalData();

        public PlatformType PlatformType { get; private set; }
        public readonly VolumeSettings VolumeSettings = new();
        public readonly MissionsSettings MissionsSettings = new();
        public readonly CoinsSettings CoinsSettings = new();
        public readonly SpaceshipSettings SpaceshipSettings = new();
        public readonly WeaponsSettings WeaponsSettings = new();
        public readonly LocalizationSettings LocalizationSettings = new();
        
        private readonly ISaver _saver = new PlayerPrefsSaver();

        public void SetPlatformType(PlatformType newPlatformType) 
            => PlatformType = newPlatformType;
        
        private PlayerGlobalData()
        {
            LoadData();

            ISettings[] settings =
            {
                VolumeSettings, MissionsSettings, CoinsSettings, 
                SpaceshipSettings, WeaponsSettings, LocalizationSettings
            };
            foreach (var setting in settings)
                setting.OnChange += SaveData;
        }
        
        public void LoadData()
        {
            if (!_saver.TryLoad(SaveKey, out var save))
            {
                //default values in settings it default save state, so we can just return
                return;
            }
            
            VolumeSettings.LoadData(save.VolumeSettingsSave);
            MissionsSettings.SetData(save.missionsSettingsSave);
            CoinsSettings.SetData(save.coinsSettingsSave);
            SpaceshipSettings.SetData(save.spaceshipSettingsSave);
            WeaponsSettings.SetData(save.weaponsSettingsesSave);
            LocalizationSettings.SetData(save.localizationSettingsSave);
        }

        public void SaveData()
        {
            var save = new PlayerGlobalDataSave(VolumeSettings, MissionsSettings, CoinsSettings, SpaceshipSettings,
                WeaponsSettings, LocalizationSettings);
            
            _saver.Save(SaveKey, save);
        }
    }
}
