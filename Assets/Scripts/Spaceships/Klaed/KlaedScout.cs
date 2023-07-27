using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaedScout : ShootingEnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedScout;
    protected override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Bullet;
}
