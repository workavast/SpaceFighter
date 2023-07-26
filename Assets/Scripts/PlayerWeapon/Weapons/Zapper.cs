using System.Collections;
using System.Collections.Generic;
using SomeStorages;
using UnityEngine;

public class Zapper : PlayerWeaponBase
{
    public override PlayerWeaponsEnum PlayerWeaponsId => PlayerWeaponsEnum.Zapper;

    protected override void Shoot()
    {
        for (int i = 0; i < ShootsPositions.Count; i++)
        {
            if (PlayerProjectilesSpawner.SpawnProjectile(PlayerProjectilesEnum.Zapper, ShootsPositions[i],
                    out PlayerProjectileBase playerProjectileBase))
            {
                (playerProjectileBase as ZapperProjectile).SetMount(ShootsPositions[i]);
            }
        }
    }
}
