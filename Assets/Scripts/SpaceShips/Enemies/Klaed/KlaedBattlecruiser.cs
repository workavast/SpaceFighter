using Projectiles.Enemy;

namespace SpaceShips.Enemies.Klaed
{
    public class KlaedBattlecruiser : ShootingEnemySpaceshipBase
    {
        public override EnemySpaceshipType PoolId => EnemySpaceshipType.KlaedBattlecruiser;
        protected override EnemyProjectileType ProjectileId => EnemyProjectileType.Wave;
    }
}
