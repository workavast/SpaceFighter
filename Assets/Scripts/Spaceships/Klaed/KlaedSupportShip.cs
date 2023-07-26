using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaedSupportShip : EnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedSupportShip;
    public override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Bullet;
}
