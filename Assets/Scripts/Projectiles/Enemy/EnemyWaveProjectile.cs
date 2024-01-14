namespace Projectiles.Enemy
{
    public class EnemyWaveProjectile : EnemyProjectileBase
    {
        public override EnemyProjectileType PoolId => EnemyProjectileType.Wave;
    
        protected override bool DestroyableOnCollision => true;
        protected override bool ReturnInPoolOnExitFromPlayArea => true;
    }
}
