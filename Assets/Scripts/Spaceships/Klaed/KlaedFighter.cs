using Projectiles.Enemy;

public class KlaedFighter : ShootingEnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedFighter;
    protected override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Bullet;
}

