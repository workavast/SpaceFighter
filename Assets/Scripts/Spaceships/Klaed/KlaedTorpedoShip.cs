using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaedTorpedoShip : EnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedTorpedoShip;
    public override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Torpedo;
}
