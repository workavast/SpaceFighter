using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using SomeStorages;

public abstract class PlayerWeaponBase : MonoBehaviour
{
    public abstract PlayerWeaponsEnum PlayerWeaponsId { get; }
    
    [Inject] protected PlayerWeaponConfig PlayerWeaponConfig;
    [Inject] protected PlayerGlobalData PlayerGlobalData;

    [SerializeField] protected List<Transform> ShootsPositions;

    protected float Damage;
    protected SomeStorageFloat FireRate;
    protected uint ShootsCountScale;
    protected bool CanShoot = true;

    private void Awake()
    {
        if (!PlayerWeaponConfig.WeaponsLevelsData.ContainsKey(PlayerWeaponsId)) throw new Exception("Undefined WeaponsEnum WeaponsId");
        
        WeaponLevel weaponLevel2 = PlayerWeaponConfig.WeaponsLevelsData[PlayerWeaponsId][(int)PlayerGlobalData.WeaponsCurrentLevels[PlayerWeaponsId]-1];

        Damage = weaponLevel2.WeaponDamage;
        FireRate = new SomeStorageFloat(weaponLevel2.FireRate);
        ShootsCountScale = weaponLevel2.ShootsCountScale;
    }

    private void Update()
    {
        FireRate.ChangeCurrentValue(Time.deltaTime);

        if (CanShoot && FireRate.IsFull)
        {
            Shoot();
            FireRate.SetCurrentValue(0);
        }
    }

    protected abstract void Shoot();

    public void StopShoot()
    {
        CanShoot = false;
    }
}
