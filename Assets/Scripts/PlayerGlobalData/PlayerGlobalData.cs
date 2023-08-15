using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalData : MonoBehaviour
{
    private struct Data
    {
        public Dictionary<int, int> MissionsData;//levelNum-starsCount
        public Dictionary<PlayerWeaponsEnum, int> CurrentWeaponsLevels;
        public int CurrentSpaceshipLevel;
        public PlayerWeaponsEnum EquippedPlayerWeapon;
        public int MoneyStarsCount; 
    }

    private static PlayerGlobalData _instance;
    
    private Data _data;

    public static IReadOnlyDictionary<int, int> MissionsData => _instance._data.MissionsData;
    public static IReadOnlyDictionary<PlayerWeaponsEnum, int> CurrentWeaponsLevels => _instance._data.CurrentWeaponsLevels;
    public static int CurrentSpaceshipLevel => _instance._data.CurrentSpaceshipLevel;
    public static PlayerWeaponsEnum EquippedPlayerWeapon => _instance._data.EquippedPlayerWeapon;
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
        SaveData();
        
        Debug.Log(_instance._data.MoneyStarsCount);
    }
    
    public static void ChangeMissionData(int missionNum, int missionStarsCount)
    {
        if (MissionsData.ContainsKey(missionNum))
        {
            if(missionStarsCount > 3) throw new Exception("Stars count more than 3");
            
            _instance._data.MissionsData[missionNum] = missionStarsCount;
        }
        else throw new Exception("Unsigned level num");
        
        SaveData();
    }

    public static void LevelUpSpaceship()
    {
        _instance._data.CurrentSpaceshipLevel++;
        
        SaveData();
    }
    
    public static void LevelUpWeapon(PlayerWeaponsEnum playerWeaponsId)
    {
        if (CurrentWeaponsLevels.ContainsKey(playerWeaponsId)) _instance._data.CurrentWeaponsLevels[playerWeaponsId]++;
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
            _instance._data.MissionsData = new DictionaryInspector<int, int>()
                { { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 0 }, { 4, 0 } };
            _instance._data.MoneyStarsCount = 1000;
            _instance._data.EquippedPlayerWeapon = PlayerWeaponsEnum.AutoCannon;
            _instance._data.CurrentSpaceshipLevel = 1;
            _instance._data.CurrentWeaponsLevels = new DictionaryInspector<PlayerWeaponsEnum, int>()
            {
                { PlayerWeaponsEnum.AutoCannon, 1 }, { PlayerWeaponsEnum.BigSpaceGun, 1 }, 
                { PlayerWeaponsEnum.Rockets, 0 }, { PlayerWeaponsEnum.Zapper, 0 }
            };
        }
    }

    public static void ChangeEquippedWeapon(PlayerWeaponsEnum weaponsEnum)
    {
        _instance._data.EquippedPlayerWeapon = weaponsEnum;
    }
}
