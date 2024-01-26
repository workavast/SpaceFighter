using System.Collections.Generic;
using PlayerWeapon;
using UnityEngine;

namespace Saves.Weapons
{
    public class WeaponSave : SaveBase<WeaponsData>
    {
        [SerializeField] public List<WeaponSavePair> CurrentWeaponsLevels;
        public PlayerWeaponType EquippedPlayerWeapon;
        
        public WeaponSave()
        {
            CurrentWeaponsLevels = default;
            EquippedPlayerWeapon = default;
        }
        
        public override void SetData(WeaponsData data)
        {
            CurrentWeaponsLevels = new List<WeaponSavePair>();
            foreach (var weaponLevelData in data.CurrentWeaponsLevels)
                CurrentWeaponsLevels.Add(new WeaponSavePair(weaponLevelData.Key, weaponLevelData.Value));
            
            EquippedPlayerWeapon = data.EquippedPlayerWeapon;
        }
    }
}