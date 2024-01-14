namespace Projectiles.Enemy
{
    public class EnemyBullet : EnemyProjectileBase
    {
        public override EnemyProjectileType PoolId => EnemyProjectileType.Bullet;
    
        protected override bool DestroyableOnCollision => true;
        protected override bool ReturnInPoolOnExitFromPlayArea => true;
    }
}
