using System.Collections;
using System.Collections.Generic;
using SomeStorages;
using UnityEngine;

public abstract class ShootingEnemySpaceshipBase : EnemySpaceshipBase
{
    protected abstract EnemyProjectilesEnum ProjectileId { get; }
    
    [Space]
    [SerializeField] protected List<Transform> shootPositions;
    [SerializeField] protected SomeStorageFloat fireRate;
    [SerializeField] protected bool canShoot;
    
    protected override void OnAwake()
    {
        base.OnAwake();
        CanShoot = canShoot;

        OnElementExtractFromPoolEvent += OnElementExtractFromPoolMethod;
        OnHandleUpdate += TryShoot;
    }
    
    protected void TryShoot()
    {
        fireRate.ChangeCurrentValue(Time.deltaTime);
        if (fireRate.IsFull)
        {
            Shoot();
            fireRate.SetCurrentValue(0);
        }
    }
    
    protected virtual void Shoot()
    {
        if(!CanShoot) return;
    
        foreach (var shootPos in shootPositions)
        {
            EnemyProjectilesSpawner.SpawnProjectile(ProjectileId, shootPos, out EnemyProjectileBase enemyProjectileBase);
        }
    }
    
    private void OnElementExtractFromPoolMethod()
    {
        CanShoot = canShoot;
        fireRate.SetCurrentValue(0);
    }
}
