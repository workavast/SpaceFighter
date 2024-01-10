using UnityEngine;

namespace Projectiles.Player
{
    public class RocketProjectile : PlayerProjectileBase
    {
        public override PlayerProjectilesEnum PoolId => PlayerProjectilesEnum.Rocket;
    
        protected override bool DestroyableOnCollision => true;
        protected override bool ReturnInPoolOnExitFromPlayArea => true;
    
        protected override void Move(float time)
        {
            transform.Translate(Vector3.up * (moveSpeed * time));
        }
    }
}
