using Projectiles.Enemy;

public class KlaedScout : ShootingEnemySpaceshipBase
{
    public override EnemySpaceshipType PoolId => EnemySpaceshipType.KlaedScout;
    protected override EnemyProjectileType ProjectileId => EnemyProjectileType.Bullet;
}
