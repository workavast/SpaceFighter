using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTorpedo : EnemyProjectileBase
{
    public override EnemyProjectilesEnum PoolId => EnemyProjectilesEnum.Torpedo;
    
    protected override bool DestroyableOnCollision => true;
    protected override bool ReturnInPoolOnExitFromPlayArea => true;
}
