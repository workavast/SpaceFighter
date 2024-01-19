using Projectiles.Player;

namespace PlayerWeapon.Weapons
{
    public class Rockets : PlayerWeaponBase
    {
        public override PlayerWeaponType PlayerWeaponId => PlayerWeaponType.Rockets;
        public override PlayerProjectileType ProjectileId => PlayerProjectileType.Rocket;
    }
}
