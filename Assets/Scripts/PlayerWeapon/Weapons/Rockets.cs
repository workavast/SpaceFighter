using Projectiles.Player;

public class Rockets : PlayerWeaponBase
{
    public override PlayerWeaponsEnum PlayerWeaponId => PlayerWeaponsEnum.Rockets;
    public override PlayerProjectilesEnum ProjectileId => PlayerProjectilesEnum.Rocket;
}
