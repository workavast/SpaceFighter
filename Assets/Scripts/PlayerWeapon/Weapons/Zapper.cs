using CastExtension;
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
                if (PlayerProjectilesManager.TrySpawnProjectile(ProjectileId, ShootsPositions[i], out var projectile))
                {
                    projectile.SetData(Damage);

                    if (projectile.TryCast<ZapperProjectile>(out ZapperProjectile zapperProjectile))
                        zapperProjectile.SetMount(ShootsPositions[i]);
                }
        }
    }
}
