using System.Collections.Generic;
using Projectiles.Enemy;

public class KlaedDreadnought : ShootingEnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedDreadnought;
    protected override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Ray;

    private List<EnemyRay> _rays = new List<EnemyRay>();

    protected override void OnAwake()
    {
        base.OnAwake();

        OnDead += ReturnRays;
    }

    private void ReturnRays()
    {
        for (int i = 0; i < _rays.Count; i++)
            _rays[i].HandleReturnInPool();

        _rays.Clear();
    }
    
    protected override void Shoot()
    {
        if(!CanShoot) return;

        foreach (var shootPos in shootPositions)
        {
            if (EnemyProjectilesManager.TrySpawnProjectile(ProjectileId, shootPos,
                    out EnemyProjectileBase enemyProjectile))
            {
                (enemyProjectile as EnemyRay).SetMount(shootPos);
                _rays.Add(enemyProjectile as EnemyRay);
            }
        }
    }
}
