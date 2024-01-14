namespace Projectiles.Player
{
    public abstract class PlayerProjectileBase : ProjectileBase<PlayerProjectileType, PlayerProjectileBase>
    {
        public void SetData(float newDamage)
        {
            damage = newDamage;
        }
    }
}
