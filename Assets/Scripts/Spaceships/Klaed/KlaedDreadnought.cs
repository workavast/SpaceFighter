using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaedDreadnought : EnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedDreadnought;
    public override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Ray;

    private List<EnemyRay> _rays = new List<EnemyRay>();

    protected override void OnAwake()
    {
        base.OnAwake();

        OnDead += ReturnRays;
    }

    private void ReturnRays()
    {
        for (int i = 0; i < _rays.Count; i++)
        {
            _rays[i].ReturnInPool();
        }
        _rays.Clear();
    }
    
    protected override void Shoot()
    {
        if(!CanShoot) return;

        foreach (var shootPos in shootPositions)
        {
            if (EnemyProjectilesSpawner.SpawnProjectile(ProjectileId, shootPos,
                    out EnemyProjectileBase enemyProjectileBase))
            {
                (enemyProjectileBase as EnemyRay).SetMount(shootPos);
                _rays.Add(enemyProjectileBase as EnemyRay);
            }
                
        }
    }
}