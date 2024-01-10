using Projectiles.Enemy;

public class KlaedFrigate : ShootingEnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedFrigate;
    protected override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.BigBullet;
}
