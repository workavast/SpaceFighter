using System;
using PlayerWeapon;

namespace Saves.Weapons
{
    [Serializable]
    public class WeaponSavePair
    {
        public PlayerWeaponType WeaponType;
        public int WeaponLevel;
        
        public WeaponSavePair(PlayerWeaponType weaponType, int weaponLevel)
        {
            WeaponType = weaponType;
            WeaponLevel = weaponLevel;
        }
    }
}