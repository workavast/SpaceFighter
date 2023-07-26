public class AutoCannon : PlayerWeaponBase
{
    public override PlayerWeaponsEnum PlayerWeaponsId => PlayerWeaponsEnum.AutoCannon;
    
    protected override void Shoot()
    {
        for (int i = 0; i < ShootsPositions.Count; i++)
        {
            PlayerProjectilesSpawner.SpawnProjectile(PlayerProjectilesEnum.AutoCannon, ShootsPositions[i],
                out PlayerProjectileBase playerProjectileBase);
        }
    }
}
