using System;
using System.Collections.Generic;
using PlayerWeapon;
using Saves.Savers;

namespace Saves.Weapons
{
    public class WeaponsSettings : SettingsBase<WeaponsData, WeaponSave>
    {
        protected override string SaveKey => "WeaponsSettings";
                                
        public IReadOnlyDictionary<PlayerWeaponType, int> CurrentWeaponsLevels => Data.CurrentWeaponsLevels;
        public PlayerWeaponType EquippedPlayerWeapon => Data.EquippedPlayerWeapon;
        
        public WeaponsSettings(ISaver saver) : base(saver) { }
        
        public void LevelUpWeapon(PlayerWeaponType playerWeaponsId)
        {
            if (Data.CurrentWeaponsLevels.ContainsKey(playerWeaponsId)) 
                Data.CurrentWeaponsLevels[playerWeaponsId]++;
            else throw new Exception("Unsigned weapon id");
            
            Save();
        }

        public void ChangeEquippedWeapon(PlayerWeaponType weaponType)
        {
            Data.EquippedPlayerWeapon = weaponType;
            Save();
        }
    }
}