using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalData : MonoBehaviour
{
    private struct Data
    {
        public DictionaryInspector<int, int> LevelsData;
        public DictionaryInspector<PlayerWeaponsEnum, int> WeaponsCurrentLevels;
        public int SpaceshipCurrentLevel;
        public PlayerWeaponsEnum CurrentSelectedPlayerWeapons;
        public int MoneyStarsCount; 
    }

    private static PlayerGlobalData _instance;
    
    private Data _data;

    public static IReadOnlyDictionary<int, int> LevelsData => _instance._data.LevelsData;
    public static IReadOnlyDictionary<PlayerWeaponsEnum, int> WeaponsCurrentLevels => _instance._data.WeaponsCurrentLevels;
    public static int SpaceshipCurrentLevel => _instance._data.SpaceshipCurrentLevel;
    public static PlayerWeaponsEnum CurrentSelectedPlayerWeapons => _instance._data.CurrentSelectedPlayerWeapons;
    public static int MoneyStarsCount => _instance._data.MoneyStarsCount;

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

    public static void ChangeMoneyStarsCount(int moneyChanged)
    {
        _instance._data.MoneyStarsCount += moneyChanged;
    }
    
    public static void ChangeLevelData(int levelNum, int starsCount)
    {
        if (LevelsData.ContainsKey(levelNum))
        {
            if(starsCount > 3) throw new Exception("Stars count more than 3");
            
            _instance._data.LevelsData[levelNum] = starsCount;
        }
        else throw new Exception("Unsigned level num");
        
        SaveData();
    }

    public static void LevelUpSpaceship()
    {
        _instance._data.SpaceshipCurrentLevel++;
        
        SaveData();
    }
    
    public static void LevelUpWeapon(PlayerWeaponsEnum playerWeaponsId)
    {
        if (WeaponsCurrentLevels.ContainsKey(playerWeaponsId)) _instance._data.WeaponsCurrentLevels[playerWeaponsId]++;
        else throw new Exception("Unsigned weapon id");
        
        SaveData();
    }

    private static void SaveData()
    {
        string str = JsonUtility.ToJson(_instance._data);
        PlayerPrefs.Save();
    }

    private static void LoadData()
    {
        Debug.Log("Loaded");
        if(PlayerPrefs.HasKey("PlayerGlobalData")) _instance._data = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("PlayerGlobalData"));
        else
        {
            Debug.Log("Created PGD data");
            _instance._data.LevelsData = new DictionaryInspector<int, int>()
                { { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 0 }, { 4, 0 } };
            _instance._data.MoneyStarsCount = 1000;
            _instance._data.CurrentSelectedPlayerWeapons = PlayerWeaponsEnum.Rockets;
            _instance._data.SpaceshipCurrentLevel = 1;
            _instance._data.WeaponsCurrentLevels = new DictionaryInspector<PlayerWeaponsEnum, int>()
            {
                { PlayerWeaponsEnum.AutoCannon, 1 }, { PlayerWeaponsEnum.BigSpaceGun, 1 }, 
                { PlayerWeaponsEnum.Rockets, 1 }, { PlayerWeaponsEnum.Zapper, 1 }
            };
        }
    }
}
