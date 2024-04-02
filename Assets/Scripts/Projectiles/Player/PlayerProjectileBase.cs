using System;

namespace Projectiles.Player
{
    public abstract class PlayerProjectileBase : ProjectileBase<PlayerProjectileType, PlayerProjectileBase>
    {
        public event Action OnSetData;
        
        public void SetData(float newDamage)
        {
            damage = newDamage;
            OnSetData?.Invoke();
        }
    }
}
