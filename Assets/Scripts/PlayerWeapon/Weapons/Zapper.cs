using Projectiles.Player;

public class Zapper : PlayerWeaponBase
{
    public override PlayerWeaponType PlayerWeaponId => PlayerWeaponType.Zapper;
    public override PlayerProjectileType ProjectileId => PlayerProjectileType.Zapper;
}
