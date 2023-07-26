using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SomeStorages;

public class BigSpaceGun : PlayerWeaponBase
{
    public override PlayerWeaponsEnum PlayerWeaponsId => PlayerWeaponsEnum.BigSpaceGun;

    protected override void Shoot()
    {
        for (int i = 0; i < ShootsPositions.Count; i++)
        {
            PlayerProjectilesSpawner.SpawnProjectile(PlayerProjectilesEnum.BigSpaceGun, ShootsPositions[i],
                out PlayerProjectileBase playerProjectileBase);
        }
    }
}

