using Projectiles.Enemy;

public class KlaedScout : ShootingEnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedScout;
    protected override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Bullet;
}
