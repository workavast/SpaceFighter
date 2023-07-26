using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : EnemyProjectileBase
{
    public override EnemyProjectilesEnum PoolId => EnemyProjectilesEnum.Bullet;
    
    protected override bool DestroyableOnCollision => true;
    protected override bool ReturnInPoolOnExitFromPlayArea => true;
}
