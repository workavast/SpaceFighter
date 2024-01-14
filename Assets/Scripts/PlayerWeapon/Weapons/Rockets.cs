using Projectiles.Player;

public class Rockets : PlayerWeaponBase
{
    public override PlayerWeaponType PlayerWeaponId => PlayerWeaponType.Rockets;
    public override PlayerProjectileType ProjectileId => PlayerProjectileType.Rocket;
}
