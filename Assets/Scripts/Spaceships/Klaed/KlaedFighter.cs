using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaedFighter : ShootingEnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedFighter;
    protected override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Bullet;
}

