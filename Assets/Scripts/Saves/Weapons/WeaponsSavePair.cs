using System;
using PlayerWeapon;

namespace Saves.Weapons
{
    [Serializable]
    public class WeaponsSavePair
    {
        public PlayerWeaponType WeaponType;
        public int WeaponLevel;
        
        public WeaponsSavePair(PlayerWeaponType weaponType, int weaponLevel)
        {
            WeaponType = weaponType;
            WeaponLevel = weaponLevel;
        }
    }
}