using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaedTorpedoShip : ShootingEnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedTorpedoShip;
    protected override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Torpedo;
}
