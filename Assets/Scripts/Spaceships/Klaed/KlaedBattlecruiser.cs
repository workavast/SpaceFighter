using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlaedBattlecruiser : EnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedBattlecruiser;
    public override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Wave;
}
