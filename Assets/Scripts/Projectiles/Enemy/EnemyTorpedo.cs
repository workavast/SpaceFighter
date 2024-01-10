namespace Projectiles.Enemy
{
    public class EnemyTorpedo : EnemyProjectileBase
    {
        public override EnemyProjectilesEnum PoolId => EnemyProjectilesEnum.Torpedo;
    
        protected override bool DestroyableOnCollision => true;
        protected override bool ReturnInPoolOnExitFromPlayArea => true;
    }
}
