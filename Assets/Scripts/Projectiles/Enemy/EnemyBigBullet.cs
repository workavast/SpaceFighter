namespace Projectiles.Enemy
{
    public class EnemyBigBullet : EnemyProjectileBase
    {
        public override EnemyProjectileType PoolId => EnemyProjectileType.BigBullet;
    
        protected override bool DestroyableOnCollision => true;
        protected override bool ReturnInPoolOnExitFromPlayArea => true;
    }
}
