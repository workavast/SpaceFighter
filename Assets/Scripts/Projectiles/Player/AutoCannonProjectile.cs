namespace Projectiles.Player
{
    public class AutoCannonProjectile : PlayerProjectileBase
    {
        public override PlayerProjectileType PoolId => PlayerProjectileType.AutoCannon;

        protected override bool DestroyableOnCollision => true;
        protected override bool ReturnInPoolOnExitFromPlayArea => true;
    }
}
