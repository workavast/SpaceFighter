using System.Collections;
using System.Collections.Generic;
using SomeStorages;
using UnityEngine;

public class Zapper : PlayerWeaponBase
{
    public override PlayerWeaponsEnum PlayerWeaponId => PlayerWeaponsEnum.Zapper;
    public override PlayerProjectilesEnum ProjectileId => PlayerProjectilesEnum.Zapper;
}
