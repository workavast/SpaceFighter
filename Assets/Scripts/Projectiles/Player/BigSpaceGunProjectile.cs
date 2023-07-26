using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpaceGunProjectile : PlayerProjectileBase
{
    public override PlayerProjectilesEnum PoolId => PlayerProjectilesEnum.BigSpaceGun;
    
    protected override bool DestroyableOnCollision => true;
    protected override bool ReturnInPoolOnExitFromPlayArea => true;
}
