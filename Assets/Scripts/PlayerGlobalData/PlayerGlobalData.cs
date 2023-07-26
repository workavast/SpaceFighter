using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalData
{
    private struct Data
    {
        public DictionaryInspector<uint, uint> LevelsData;
        public DictionaryInspector<PlayerWeaponsEnum, uint> WeaponsCurrentLevels;
        public uint SpaceshipCurrentLevel;
        public PlayerWeaponsEnum CurrentSelectedPlayerWeapons;
        public uint MoneyCount; 
    }

    private Data _data;

    public IReadOnlyDictionary<uint, uint> LevelsData => _data.LevelsData;
    public IReadOnlyDictionary<PlayerWeaponsEnum, uint> WeaponsCurrentLevels => _data.WeaponsCurrentLevels;
    public uint SpaceshipCurrentLevel => _data.SpaceshipCurrentLevel;
    public PlayerWeaponsEnum CurrentSelectedPlayerWeapons => _data.CurrentSelectedPlayerWeapons;
    public uint MoneyCount => _data.MoneyCount;

    public void ChangeMoneyCount(uint moneyChanged)
    {
        _data.MoneyCount += moneyChanged;
    }
    
    public void ChangeLevelData(uint levelNum, uint starsCount)
    {
        if (LevelsData.ContainsKey(levelNum))
        {
            if(starsCount > 3) throw new Exception("Stars count more than 3");
            
            _data.LevelsData[levelNum] = starsCount;
        }
        else throw new Exception("Unsigned level num");
        
        SaveData();
    }

    public void LevelUpSpaceship()
    {
        _data.SpaceshipCurrentLevel++;
        
        SaveData();
    }
    
    public void LevelUpWeapon(PlayerWeaponsEnum playerWeaponsId)
    {
        if (WeaponsCurrentLevels.ContainsKey(playerWeaponsId)) _data.WeaponsCurrentLevels[playerWeaponsId]++;
        else throw new Exception("Unsigned weapon id");
        
        SaveData();
    }

    public void SaveData()
    {
        string str = JsonUtility.ToJson(_data);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        Debug.Log("Loaded");
        if(PlayerPrefs.HasKey("PlayerGlobalData")) _data = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("PlayerGlobalData"));
        else
        {
            _data.LevelsData = new DictionaryInspector<uint, uint>()
                { { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 0 }, { 4, 0 } };
            _data.MoneyCount = 1000;
            _data.CurrentSelectedPlayerWeapons = PlayerWeaponsEnum.Rockets;
            _data.SpaceshipCurrentLevel = 1;
            _data.WeaponsCurrentLevels = new DictionaryInspector<PlayerWeaponsEnum, uint>()
            {
                { PlayerWeaponsEnum.AutoCannon, 1 }, { PlayerWeaponsEnum.BigSpaceGun, 1 }, 
                { PlayerWeaponsEnum.Rockets, 1 }, { PlayerWeaponsEnum.Zapper, 1 }
            };
        }
    }
}
