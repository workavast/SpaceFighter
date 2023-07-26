using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SomeStorages;

public class Rockets : PlayerWeaponBase
{
    public override PlayerWeaponsEnum PlayerWeaponsId => PlayerWeaponsEnum.Rockets;

    protected override void Shoot()
    {
        for (int i = 0; i < ShootsPositions.Count; i++)
        {
            PlayerProjectilesSpawner.SpawnProjectile(PlayerProjectilesEnum.Rocket, ShootsPositions[i],
                out PlayerProjectileBase playerProjectileBase);
        }
    }
}
