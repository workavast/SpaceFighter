namespace Projectiles.Player
{
    public class BigSpaceGunProjectile : PlayerProjectileBase
    {
        public override PlayerProjectileType PoolId => PlayerProjectileType.BigSpaceGun;
    
        protected override bool DestroyableOnCollision => true;
        protected override bool ReturnInPoolOnExitFromPlayArea => true;
    }
}
