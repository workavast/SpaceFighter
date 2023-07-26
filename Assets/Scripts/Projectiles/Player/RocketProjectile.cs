using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : PlayerProjectileBase
{
    public override PlayerProjectilesEnum PoolId => PlayerProjectilesEnum.Rocket;
    
    protected override bool DestroyableOnCollision => true;
    protected override bool ReturnInPoolOnExitFromPlayArea => true;
    
    protected override void Move()
    {
        transform.Translate(Vector3.up * (moveSpeed * Time.deltaTime));
    }
}
