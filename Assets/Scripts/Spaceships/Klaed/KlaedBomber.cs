using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaedBomber : EnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedBomber;
    public override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Bullet;
}
