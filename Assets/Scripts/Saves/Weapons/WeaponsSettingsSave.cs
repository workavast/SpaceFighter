using System;
using System.Collections.Generic;
using PlayerWeapon;
using UnityEngine;

namespace Saves.Weapons
{
    [Serializable]
    public class WeaponsSettingsSave
    {
        [SerializeField] public List<WeaponsSavePair> CurrentWeaponsLevels;
        public PlayerWeaponType EquippedPlayerWeapon;

        public WeaponsSettingsSave(WeaponsSettings settings)
        {
            CurrentWeaponsLevels = new List<WeaponsSavePair>();
            foreach (var weaponLevelData in settings.CurrentWeaponsLevels)
                CurrentWeaponsLevels.Add(new WeaponsSavePair(weaponLevelData.Key, weaponLevelData.Value));
            
            EquippedPlayerWeapon = settings.EquippedPlayerWeapon;
        }
    }
}