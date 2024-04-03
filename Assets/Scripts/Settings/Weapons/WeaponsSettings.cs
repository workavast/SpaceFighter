using System;
using System.Collections.Generic;
using PlayerWeapon;

namespace Settings.Weapons
{
    public class WeaponsSettings : ISettings
    {
        public Dictionary<PlayerWeaponType, int> WeaponsLevels = new Dictionary<PlayerWeaponType, int>()
        {
            { PlayerWeaponType.AutoCannon, 1 }, 
            { PlayerWeaponType.BigSpaceGun, 0 }, 
            { PlayerWeaponType.Rockets, 0 },
            { PlayerWeaponType.Zapper, 0 }
        };
        public PlayerWeaponType EquippedPlayerWeapon = PlayerWeaponType.AutoCannon;
        public IReadOnlyDictionary<PlayerWeaponType, int> CurrentWeaponsLevels => WeaponsLevels;
        
        public event Action OnChange;
        
        public void SetData(WeaponsSettingsSave save)
        {
            WeaponsLevels = new Dictionary<PlayerWeaponType, int>();
            foreach (var weaponLevelData in save.CurrentWeaponsLevels)
                WeaponsLevels.Add(weaponLevelData.WeaponType, weaponLevelData.WeaponLevel);

            EquippedPlayerWeapon = save.EquippedPlayerWeapon;
        }

        public void LevelUpWeapon(PlayerWeaponType playerWeaponsId)
        {
            if (WeaponsLevels.ContainsKey(playerWeaponsId)) 
                WeaponsLevels[playerWeaponsId]++;
            else throw new Exception("Unsigned weapon id");
            
            OnChange?.Invoke();
        }

        public void ChangeEquippedWeapon(PlayerWeaponType weaponType)
        {
            EquippedPlayerWeapon = weaponType;
            OnChange?.Invoke();
        }
    }
}