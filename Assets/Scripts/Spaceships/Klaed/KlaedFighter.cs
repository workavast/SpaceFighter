using Projectiles.Enemy;

public class KlaedFighter : ShootingEnemySpaceshipBase
{
    public override EnemySpaceshipType PoolId => EnemySpaceshipType.KlaedFighter;
    protected override EnemyProjectileType ProjectileId => EnemyProjectileType.Bullet;
}

