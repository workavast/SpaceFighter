using Projectiles.Enemy;

namespace SpaceShips.Enemies.Klaed
{
    public class KlaedScout : ShootingEnemySpaceshipBase
    {
        public override EnemySpaceshipType PoolId => EnemySpaceshipType.KlaedScout;
        protected override EnemyProjectileType ProjectileId => EnemyProjectileType.Bullet;
    }
}
