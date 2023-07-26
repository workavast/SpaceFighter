using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaedFrigate : EnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedFrigate;
    public override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.BigBullet;
}
