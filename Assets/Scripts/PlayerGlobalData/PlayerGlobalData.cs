using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalData : MonoBehaviour
{
    public struct MissionCell
    {
        public bool star_1 { get; private set; }
        public bool star_2 { get; private set; }
        public bool star_3 { get; private set; }

        public MissionCell(bool star1,bool star2, bool star3)
        {
            star_1 = star1;
            star_2 = star2;
            star_3 = star3;
        }
    }

    private struct Data
    {
        public List<MissionCell> MissionsData;//levelNum-starsCount
        public Dictionary<PlayerWeaponType, int> CurrentWeaponsLevels;
        public int CurrentSpaceshipLevel;
        public PlayerWeaponType EquippedPlayerWeapon;
        public int MoneyStarsCount; 
    }

    private static PlayerGlobalData _instance;
    
    private Data _data;

    public static IReadOnlyList<MissionCell> MissionsData => _instance._data.MissionsData;
    public static IReadOnlyDictionary<PlayerWeaponType, int> CurrentWeaponsLevels => _instance._data.CurrentWeaponsLevels;
    public static int CurrentSpaceshipLevel => _instance._data.CurrentSpaceshipLevel;
    public static PlayerWeaponType EquippedPlayerWeapon => _instance._data.EquippedPlayerWeapon;
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
    
    public static void ChangeMissionData(int missionNum, int starCount)
    {
        if (missionNum < 0 && missionNum > MissionsData.Count) throw new Exception("Unsigned level num");
        if (starCount < 0) throw new Exception("Unsigned level num");

        MissionCell newMissionCell;
        switch (starCount)
        {
            case 0: newMissionCell = new MissionCell();break;
            case 1: newMissionCell = new MissionCell(true, false, false); break;
            case 2: newMissionCell = new MissionCell(true, true, false); break;
            default: newMissionCell = new MissionCell(true, true, true); break;
        }
        
        MissionCell oldMissionCell = _instance._data.MissionsData[missionNum];
        _instance._data.MissionsData[missionNum] = new MissionCell(oldMissionCell.star_1 || newMissionCell.star_1, oldMissionCell.star_2 || newMissionCell.star_2, oldMissionCell.star_3 || newMissionCell.star_3);
        
        SaveData();
    }

    public static void LevelUpSpaceship()
    {
        _instance._data.CurrentSpaceshipLevel++;
        
        SaveData();
    }
    
    public static void LevelUpWeapon(PlayerWeaponType playerWeaponsId)
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
            _instance._data.MissionsData = new List<MissionCell>();

            for (int i = 0; i < 21; i++)
            {
                _instance._data.MissionsData.Add(new MissionCell());
            }
            _instance._data.MissionsData[0] = new MissionCell(true,true, false);

            _instance._data.MoneyStarsCount = 1000;
            _instance._data.EquippedPlayerWeapon = PlayerWeaponType.AutoCannon;
            _instance._data.CurrentSpaceshipLevel = 1;
            _instance._data.CurrentWeaponsLevels = new DictionaryInspector<PlayerWeaponType, int>()
            {
                { PlayerWeaponType.AutoCannon, 1 }, { PlayerWeaponType.BigSpaceGun, 1 }, 
                { PlayerWeaponType.Rockets, 0 }, { PlayerWeaponType.Zapper, 0 }
            };
        }
    }

    public static void ChangeEquippedWeapon(PlayerWeaponType weaponType)
    {
        _instance._data.EquippedPlayerWeapon = weaponType;
    }
}
