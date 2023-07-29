using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SomeStorages;

public class Rockets : PlayerWeaponBase
{
    public override PlayerWeaponsEnum PlayerWeaponId => PlayerWeaponsEnum.Rockets;
    public override PlayerProjectilesEnum ProjectileId => PlayerProjectilesEnum.Rocket;
}
