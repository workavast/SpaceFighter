using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalData : MonoBehaviour
{
    private static PlayerGlobalData _instance;
    
    private Data _data;

    public static IReadOnlyList<int> MissionsStarsData => _instance._data.MissionsStarsData;
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
    
    public static void ChangeMissionData(int missionIndex, int starCount)
    {
        if (missionIndex < 0 && missionIndex > MissionsStarsData.Count) throw new Exception("Unsigned level num");
        if (starCount < 0)
        {
            starCount = 0;
            Debug.LogError("Unsigned level num");
        }
        if (starCount > 3)
        {
            starCount = 3;
            Debug.LogError("Unsigned level num");
        }

        var oldStarsCount = _instance._data.MissionsStarsData[missionIndex];
        if(starCount <= oldStarsCount) return;

        _instance._data.MissionsStarsData[missionIndex] = starCount;
        
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
        SavingData savingData = new SavingData(_instance._data);
        var str = JsonUtility.ToJson(savingData);
        PlayerPrefs.SetString("PlayerGlobalData", str);
        PlayerPrefs.Save();
    }

    private static void LoadData()
    {
        Debug.Log("Loaded");
        if (PlayerPrefs.HasKey("PlayerGlobalData"))
        {
            var savingStr = PlayerPrefs.GetString("PlayerGlobalData");
            var savingData = JsonUtility.FromJson<SavingData>(savingStr);
            _instance._data = new Data(savingData);
        }
        else
        {
            Debug.Log("Created PGD data");
            
            _instance._data.MissionsStarsData = new List<int>();
            for (int i = 0; i < 21; i++)
                _instance._data.MissionsStarsData.Add(0);

            _instance._data.MoneyStarsCount = 1000;
            _instance._data.EquippedPlayerWeapon = PlayerWeaponType.AutoCannon;
            _instance._data.CurrentSpaceshipLevel = 1;
            _instance._data.CurrentWeaponsLevels = new DictionaryInspector<PlayerWeaponType, int>()
            {
                { PlayerWeaponType.AutoCannon, 1 }, 
                { PlayerWeaponType.BigSpaceGun, 1 }, 
                { PlayerWeaponType.Rockets, 0 },
                { PlayerWeaponType.Zapper, 0 }
            };
        }
    }

    public static void ChangeEquippedWeapon(PlayerWeaponType weaponType)
    {
        _instance._data.EquippedPlayerWeapon = weaponType;
        SaveData();
    }
    
    [Serializable]
    private struct Pair
    {
        public PlayerWeaponType WeaponType;
        public int WeaponLevel;

        public Pair(PlayerWeaponType weaponType, int weaponLevel)
        {
            WeaponType = weaponType;
            WeaponLevel = weaponLevel;
        }
    }
    
    [Serializable]
    private struct SavingData
    {
        public List<int> MissionsStarsData;
        public List<Pair> CurrentWeaponsLevels;
        public int CurrentSpaceshipLevel;
        public PlayerWeaponType EquippedPlayerWeapon;
        public int MoneyStarsCount;
        
        public SavingData(Data data)
        {
            MissionsStarsData = data.MissionsStarsData;

            CurrentWeaponsLevels = new List<Pair>();
            foreach (var weaponLevelData in data.CurrentWeaponsLevels)
                CurrentWeaponsLevels.Add(new Pair(weaponLevelData.Key, weaponLevelData.Value));
            
            CurrentSpaceshipLevel = data.CurrentSpaceshipLevel;
            EquippedPlayerWeapon = data.EquippedPlayerWeapon;
            MoneyStarsCount = data.MoneyStarsCount;
        }
    }
    
    private struct Data
    {
        public List<int> MissionsStarsData;//levelNum-starsCount
        public Dictionary<PlayerWeaponType, int> CurrentWeaponsLevels;
        public int CurrentSpaceshipLevel;
        public PlayerWeaponType EquippedPlayerWeapon;
        public int MoneyStarsCount; 
        
        public Data(SavingData savingData)
        {
            MissionsStarsData = savingData.MissionsStarsData;
            
            CurrentWeaponsLevels = new Dictionary<PlayerWeaponType, int>();
            foreach (var weaponLevelData in savingData.CurrentWeaponsLevels)
                CurrentWeaponsLevels.Add(weaponLevelData.WeaponType, weaponLevelData.WeaponLevel);
            
            CurrentSpaceshipLevel = savingData.CurrentSpaceshipLevel;
            EquippedPlayerWeapon = savingData.EquippedPlayerWeapon;
            MoneyStarsCount = savingData.MoneyStarsCount;
        }
    }
}
