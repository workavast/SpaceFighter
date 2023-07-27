using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaedFrigate : ShootingEnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedFrigate;
    protected override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.BigBullet;
}
