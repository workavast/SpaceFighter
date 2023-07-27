using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaedBattlecruiser : ShootingEnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedBattlecruiser;
    protected override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Wave;
}
