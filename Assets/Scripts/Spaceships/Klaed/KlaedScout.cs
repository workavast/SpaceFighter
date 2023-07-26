using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaedScout : EnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedScout;
    public override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Bullet;
}
