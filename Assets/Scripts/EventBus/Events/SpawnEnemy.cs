using Configs.Missions;
using EventBusExtension;

namespace EventBus.Events
{
    public struct SpawnEnemy : IEvent
    {
        public EnemySpaceshipType EnemySpaceshipType { get; private set; }
        public EnemyGroupConfig EnemyGroupConfig { get; private set; }

        public SpawnEnemy(EnemySpaceshipType enemySpaceshipType, EnemyGroupConfig enemyGroupConfig)
        {
            EnemySpaceshipType = enemySpaceshipType;
            EnemyGroupConfig = enemyGroupConfig;
        }
        
        public SpawnEnemy(int enemySpaceshipIndex, EnemyGroupConfig enemyGroupConfig)
        {
            EnemySpaceshipType = enemyGroupConfig.enemySubgroup[enemySpaceshipIndex];
            EnemyGroupConfig = enemyGroupConfig;
        }
    }
}