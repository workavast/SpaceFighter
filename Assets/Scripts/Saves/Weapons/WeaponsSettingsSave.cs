using System;
using System.Collections.Generic;
using PlayerWeapon;
using UnityEngine;

namespace Saves.Weapons
{
    [Serializable]
    public class WeaponsSettingsSave
    {
        public List<WeaponsSavePair> CurrentWeaponsLevels = new()
        {
            new WeaponsSavePair(PlayerWeaponType.AutoCannon, 1),
            new WeaponsSavePair(PlayerWeaponType.BigSpaceGun, 0),
            new WeaponsSavePair(PlayerWeaponType.Rockets, 0),
            new WeaponsSavePair(PlayerWeaponType.Zapper, 0)
        };
        public PlayerWeaponType EquippedPlayerWeapon = PlayerWeaponType.AutoCannon;

        public WeaponsSettingsSave()
        {
            CurrentWeaponsLevels = new()
            {
                new WeaponsSavePair(PlayerWeaponType.AutoCannon, 1),
                new WeaponsSavePair(PlayerWeaponType.BigSpaceGun, 0),
                new WeaponsSavePair(PlayerWeaponType.Rockets, 0),
                new WeaponsSavePair(PlayerWeaponType.Zapper, 0)
            };
            EquippedPlayerWeapon = PlayerWeaponType.AutoCannon;
        }
        
        public WeaponsSettingsSave(WeaponsSettings settings)
        {
            CurrentWeaponsLevels = new List<WeaponsSavePair>();
            Debug.Log(settings.CurrentWeaponsLevels.Count);
            foreach (var weaponLevelData in settings.CurrentWeaponsLevels)
                CurrentWeaponsLevels.Add(new WeaponsSavePair(weaponLevelData.Key, weaponLevelData.Value));
            
            EquippedPlayerWeapon = settings.EquippedPlayerWeapon;
        }
    }
}