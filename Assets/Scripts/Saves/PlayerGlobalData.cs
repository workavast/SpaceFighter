using Saves.Coins;
using Saves.Localization;
using Saves.Missions;
using Saves.Savers;
using Saves.Spaceship;
using Saves.Volume;
using Saves.Weapons;
using UnityEngine;

namespace Saves
{
    public class PlayerGlobalData : MonoBehaviour
    {
        private static PlayerGlobalData _instance;
    
        private readonly VolumeSettings _volumeSettings = new(new PlayerPrefsSaver());
        private readonly MissionsSettings _missionsSettings = new(new PlayerPrefsSaver());
        private readonly CoinsSettings _coinsSettings = new(new PlayerPrefsSaver());
        private readonly SpaceshipSettings _spaceshipSettings = new(new PlayerPrefsSaver());
        private readonly WeaponsSettings _weaponsSettings = new(new PlayerPrefsSaver());
        private readonly LocalizationSettings _localizationSettings = new(new PlayerPrefsSaver());
        
        public static VolumeSettings VolumeSettings => _instance._volumeSettings;
        public static MissionsSettings MissionsSettings => _instance._missionsSettings;
        public static CoinsSettings CoinsSettings => _instance._coinsSettings;
        public static SpaceshipSettings SpaceshipSettings => _instance._spaceshipSettings;
        public static WeaponsSettings WeaponsSettings => _instance._weaponsSettings;
        public static LocalizationSettings LocalizationSettings => _instance._localizationSettings;
        
        private void Awake()
        {
            if (_instance)
            {
                Destroy(this);
                return;
            }
        
            _instance = this;
            LoadData();
        
            DontDestroyOnLoad(this);
        }
        
        private void LoadData()
        {
            _volumeSettings.Load();
            _missionsSettings.Load();
            _coinsSettings.Load();
            _spaceshipSettings.Load();
            _weaponsSettings.Load();
            _localizationSettings.Load();
        }
    }
}
