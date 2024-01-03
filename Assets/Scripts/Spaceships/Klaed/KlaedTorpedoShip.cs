public class KlaedTorpedoShip : ShootingEnemySpaceshipBase
{
    public override EnemySpaceshipsEnum PoolId => EnemySpaceshipsEnum.KlaedTorpedoShip;
    protected override EnemyProjectilesEnum ProjectileId => EnemyProjectilesEnum.Torpedo;
}
