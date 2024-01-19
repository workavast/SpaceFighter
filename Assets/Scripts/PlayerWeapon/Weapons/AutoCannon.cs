using Projectiles.Player;

namespace PlayerWeapon.Weapons
{
    public class AutoCannon : PlayerWeaponBase
    {
        public override PlayerWeaponType PlayerWeaponId => PlayerWeaponType.AutoCannon;
        public override PlayerProjectileType ProjectileId => PlayerProjectileType.AutoCannon;
    }
}
