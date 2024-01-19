using Projectiles.Enemy;

namespace SpaceShips.Enemies.Klaed
{
    public class KlaedFrigate : ShootingEnemySpaceshipBase
    {
        public override EnemySpaceshipType PoolId => EnemySpaceshipType.KlaedFrigate;
        protected override EnemyProjectileType ProjectileId => EnemyProjectileType.BigBullet;
    }
}
