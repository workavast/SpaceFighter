namespace Projectiles.Enemy
{
    public class EnemyBigBullet : EnemyProjectileBase
    {
        public override EnemyProjectilesEnum PoolId => EnemyProjectilesEnum.BigBullet;
    
        protected override bool DestroyableOnCollision => true;
        protected override bool ReturnInPoolOnExitFromPlayArea => true;
    }
}
