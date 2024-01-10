namespace Projectiles.Player
{
    public abstract class PlayerProjectileBase : ProjectileBase<PlayerProjectilesEnum, PlayerProjectileBase>
    {
        public void SetData(float newDamage)
        {
            damage = newDamage;
        }
    }
}
