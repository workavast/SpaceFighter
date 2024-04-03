using CastLibrary;
using Projectiles.Player;

namespace PlayerWeapon.Weapons
{
    public class Zapper : PlayerWeaponBase
    {
        public override PlayerWeaponType PlayerWeaponId => PlayerWeaponType.Zapper;
        public override PlayerProjectileType ProjectileId => PlayerProjectileType.Zapper;

        protected override void Shoot()
        {
            for (int i = 0; i < ShootsPositions.Count; i++)
            {
                var projectile = PlayerProjectilesFactory.Create(ProjectileId, ShootsPositions[i]);
                projectile.SetData(Damage);

                if (projectile.TryCast(out ZapperProjectile zapperProjectile))
                    zapperProjectile.SetMount(ShootsPositions[i]);
            }
        }
    }
}
