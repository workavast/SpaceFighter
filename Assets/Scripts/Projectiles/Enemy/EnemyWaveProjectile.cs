using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveProjectile : EnemyProjectileBase
{
    public override EnemyProjectilesEnum PoolId => EnemyProjectilesEnum.Wave;
    
    protected override bool DestroyableOnCollision => true;
    protected override bool ReturnInPoolOnExitFromPlayArea => true;
}
