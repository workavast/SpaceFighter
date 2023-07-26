using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaedFighter : EnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedFighter;
    public override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Bullet;
}

