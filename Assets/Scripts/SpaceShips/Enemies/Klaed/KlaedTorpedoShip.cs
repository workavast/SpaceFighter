using Projectiles.Enemy;

namespace SpaceShips.Enemies.Klaed
{
    public class KlaedTorpedoShip : ShootingEnemySpaceshipBase
    {
        public override EnemySpaceshipType PoolId => EnemySpaceshipType.KlaedTorpedoShip;
        protected override EnemyProjectileType ProjectileId => EnemyProjectileType.Torpedo;
    }
}
