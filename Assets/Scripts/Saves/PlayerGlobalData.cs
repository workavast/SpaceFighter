using System;
using Initializers;
using Saves.Coins;
using Saves.Localization;
using Saves.Missions;
using Saves.Spaceship;
using Saves.Volume;
using Saves.Weapons;
using UnityEngine;
using YG;

namespace Saves
{
    public class PlayerGlobalData
    {
        private static PlayerGlobalData _instance;
        public static PlayerGlobalData Instance => _instance ??= new PlayerGlobalData();

        public event Action OnInit;
        
        public PlatformType PlatformType { get; private set; }
        public readonly VolumeSettings VolumeSettings = new();
        public readonly MissionsSettings MissionsSettings = new();
        public readonly CoinsSettings CoinsSettings = new();
        public readonly SpaceshipSettings SpaceshipSettings = new();
        public readonly WeaponsSettings WeaponsSettings = new();
        public readonly LocalizationSettings LocalizationSettings = new();
        
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
            if (YandexGame.savesData.isFirstSession)
            {
                YandexGame.savesData.isFirstSession = false;
                
                // var save2 = new PlayerGlobalDataSave();
                //
                // YandexGame.savesData.playerGlobalDataSave = save2;
                // YandexGame.SaveProgress();
                
                SaveData();
                Debug.Log("-||- First get data");
                //default values in settings is a default save values, so we can just return
                return;
            }

            var save = YandexGame.savesData.playerGlobalDataSave;
            Debug.Log("-||- Not first get data");
            VolumeSettings.LoadData(save.volumeSettingsSave);
            MissionsSettings.SetData(save.missionsSettingsSave);
            CoinsSettings.SetData(save.coinsSettingsSave);
            SpaceshipSettings.SetData(save.spaceshipSettingsSave);
            WeaponsSettings.SetData(save.weaponsSettingsSave);
            LocalizationSettings.SetData(save.localizationSettingsSave);
        }

        public void ResetSaves()
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
