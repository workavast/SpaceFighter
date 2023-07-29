using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using SomeStorages;

public abstract class PlayerWeaponBase : MonoBehaviour
{
    public abstract PlayerWeaponsEnum PlayerWeaponId { get; }
    public abstract PlayerProjectilesEnum ProjectileId { get; }
    
    [Inject] protected PlayerWeaponConfig PlayerWeaponConfig;
    [Inject] protected PlayerGlobalData PlayerGlobalData;

    [SerializeField] protected List<Transform> ShootsPositions;

    protected float Damage;
    protected SomeStorageFloat FireRate;
    protected uint ShootsCountScale;
    protected bool CanShoot = true;

    private void Awake()
    {
        if (!PlayerWeaponConfig.WeaponsLevelsData.ContainsKey(PlayerWeaponId)) throw new Exception("Undefined WeaponsEnum WeaponsId");
        
        WeaponLevel weaponLevel = PlayerWeaponConfig.WeaponsLevelsData[PlayerWeaponId][(int)PlayerGlobalData.WeaponsCurrentLevels[PlayerWeaponId]-1];

        Damage = weaponLevel.WeaponDamage;
        FireRate = new SomeStorageFloat(weaponLevel.FireRate);
        ShootsCountScale = weaponLevel.ShootsCountScale;
    }

    public void HandleUpdate()
    {
        FireRate.ChangeCurrentValue(Time.deltaTime);

        if (CanShoot && FireRate.IsFull)
        {
            Shoot();
            FireRate.SetCurrentValue(0);
        }
    }

    protected virtual void Shoot()
    {
        for (int i = 0; i < ShootsPositions.Count; i++)
        {
            if (PlayerProjectilesSpawner.SpawnProjectile(ProjectileId, ShootsPositions[i],
                    out PlayerProjectileBase playerProjectileBase))
            {
                playerProjectileBase.SetData(Damage);
            }
        }
    }

    public void StopShoot()
    {
        CanShoot = false;
    }
}
