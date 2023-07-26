using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCannonProjectile : PlayerProjectileBase
{
    public override PlayerProjectilesEnum PoolId => PlayerProjectilesEnum.AutoCannon;

    protected override bool DestroyableOnCollision => true;
    protected override bool ReturnInPoolOnExitFromPlayArea => true;
}
