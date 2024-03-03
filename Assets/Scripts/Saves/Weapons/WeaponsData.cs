using System.Collections.Generic;
using PlayerWeapon;

namespace Saves.Weapons
{
    public class WeaponsData : DataBase<WeaponSave>
    {
        public Dictionary<PlayerWeaponType, int> CurrentWeaponsLevels = new Dictionary<PlayerWeaponType, int>()
        {
            { PlayerWeaponType.AutoCannon, 1 }, 
            { PlayerWeaponType.BigSpaceGun, 0 }, 
            { PlayerWeaponType.Rockets, 0 },
            { PlayerWeaponType.Zapper, 0 }
        };
        public PlayerWeaponType EquippedPlayerWeapon = PlayerWeaponType.AutoCannon;

        public override void SetData(WeaponSave save)
        {
            CurrentWeaponsLevels = new Dictionary<PlayerWeaponType, int>();
            foreach (var weaponLevelData in save.CurrentWeaponsLevels)
                CurrentWeaponsLevels.Add(weaponLevelData.WeaponType, weaponLevelData.WeaponLevel);

            EquippedPlayerWeapon = save.EquippedPlayerWeapon;
        }
    }
}