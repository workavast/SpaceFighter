namespace Projectiles.Enemy
{
    public class EnemyTorpedo : EnemyProjectileBase
    {
        public override EnemyProjectileType PoolId => EnemyProjectileType.Torpedo;
    
        protected override bool DestroyableOnCollision => true;
        protected override bool ReturnInPoolOnExitFromPlayArea => true;
    }
}
