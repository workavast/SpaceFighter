using Projectiles.Enemy;

public class KlaedTorpedoShip : ShootingEnemySpaceshipBase
{
    public override EnemySpaceshipType PoolId => EnemySpaceshipType.KlaedTorpedoShip;
    protected override EnemyProjectileType ProjectileId => EnemyProjectileType.Torpedo;
}
