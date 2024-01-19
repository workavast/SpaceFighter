using Projectiles.Enemy;

namespace SpaceShips.Enemies.Klaed
{
    public class KlaedFighter : ShootingEnemySpaceshipBase
    {
        public override EnemySpaceshipType PoolId => EnemySpaceshipType.KlaedFighter;
        protected override EnemyProjectileType ProjectileId => EnemyProjectileType.Bullet;
    }
}

