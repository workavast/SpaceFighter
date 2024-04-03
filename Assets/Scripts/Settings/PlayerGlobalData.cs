using System;
using Initializers;
using Settings.Coins;
using Settings.Localization;
using Settings.Missions;
using Settings.Spaceship;
using Settings.Volume;
using Settings.Weapons;
using UnityEngine;
using YG;

namespace Settings
{
    public class PlayerGlobalData
    {
        private static PlayerGlobalData _instance;
        public static PlayerGlobalData Instance => _instance ??= new PlayerGlobalData();

        public PlatformType PlatformType { get; private set; } = PlatformType.Desktop;
        public readonly VolumeSettings VolumeSettings = new();
        public readonly MissionsSettings MissionsSettings = new();
        public readonly CoinsSettings CoinsSettings = new();
        public readonly SpaceshipSettings SpaceshipSettings = new();
        public readonly WeaponsSettings WeaponsSettings = new();
        public readonly LocalizationSettings LocalizationSettings = new();
     
        public bool IsFirstSession { get; private set; }
        
        public event Action OnInit;

        public void SetPlatformType(PlatformType newPlatformType) 
            => PlatformType = newPlatformType;

        public void Initialize()
        {
            YandexGame.GetDataEvent += GetData;
            YandexGame.GetDataEvent += SubsAfterFirstLoad;

            Debug.Log("-||- load start");
            YandexGame.LoadProgress();
        }
        
        private void GetData()
        {
            IsFirstSession = YandexGame.savesData.isFirstSession;
            if (IsFirstSession)
            {
                YandexGame.savesData.isFirstSession = false;
                
                SaveData();
                Debug.Log("-||- First get data");
                //default values in settings is a default save values, so we can just return
                return;
            }
            
            Debug.Log("-||- Not first get data");
            var save = YandexGame.savesData.playerGlobalDataSave;
            VolumeSettings.LoadData(save.volumeSettingsSave);
            MissionsSettings.SetData(save.missionsSettingsSave);
            CoinsSettings.SetData(save.coinsSettingsSave);
            SpaceshipSettings.SetData(save.spaceshipSettingsSave);
            WeaponsSettings.SetData(save.weaponsSettingsSave);
            LocalizationSettings.SetData(save.localizationSettingsSave);
        }

        public static void ResetSaves()
        {
            Debug.Log("-||- Reset saves");
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }
        
        private void SaveData()
        {
            Debug.Log("-||- SaveData");

            var save = new PlayerGlobalDataSave(VolumeSettings, MissionsSettings, CoinsSettings, SpaceshipSettings,
                WeaponsSettings, LocalizationSettings);
            
            YandexGame.savesData.playerGlobalDataSave = save;
            YandexGame.SaveProgress();
        }
        
        private void SubsAfterFirstLoad()
        {
            Debug.Log("-||- SubsAfterFirstLoad");
            
            YandexGame.GetDataEvent -= SubsAfterFirstLoad;

            ISettings[] settings =
            {
                VolumeSettings, MissionsSettings, CoinsSettings, 
                SpaceshipSettings, WeaponsSettings, LocalizationSettings
            };
            foreach (var setting in settings)
                setting.OnChange += SaveData;
            
            OnInit?.Invoke();
        }
    }
}
