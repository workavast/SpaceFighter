using System;
using Configs.Missions;
using EventBusEvents;
using EventBusExtension;
using Factories;
using SomeStorages;
using TimerExtension;

namespace WaveSpawnerSystem
{
    public class WaveSubgroupSpawner : IHandleUpdate
    {
        private readonly SomeStorageInt _enemiesCounter;
        private readonly SomeStorageFloat _spawnPause;
        private readonly Timer _timer;
        private readonly EnemyGroupConfig _enemyGroupConfig;
        private readonly EnemySpaceshipsFactory _enemySpaceshipsFactory;

        public event Action OnEndSpawn;
        private event Action<float> OnHandleUpdate;
        
        public WaveSubgroupSpawner(EnemyGroupConfig enemyGroupConfig, EnemySpaceshipsFactory enemySpaceshipsFactory)
        {
            _enemyGroupConfig = enemyGroupConfig;
            _enemiesCounter = new SomeStorageInt(_enemyGroupConfig.EnemySubgroup.Count);
            _enemySpaceshipsFactory = enemySpaceshipsFactory;
            
            _timer = new Timer(_enemyGroupConfig.EnemiesTimePause);
            _timer.OnTimerEnd += PauseEnd;

            OnHandleUpdate += _timer.Tick;
        }
        
        public void HandleUpdate(float time) => OnHandleUpdate?.Invoke(time);

        public void Reset()
        {
            OnHandleUpdate = null;
            _enemiesCounter.SetCurrentValue(0);
            _timer.Reset();
            
            OnHandleUpdate += _timer.Tick;
        }
        
        private void PauseEnd()
        {
            SpawnEnemy();
            _timer.Reset();
        }
        
        private void SpawnEnemy()
        {
            var enemy = _enemySpaceshipsFactory.Create(_enemyGroupConfig.EnemySubgroup[_enemiesCounter.CurrentValue]);
            enemy.SetWaveData(_enemyGroupConfig);
            
            _enemiesCounter.ChangeCurrentValue(1);
            if (_enemiesCounter.IsFull)
            {
                OnEndSpawn?.Invoke();
                OnHandleUpdate = null;
            }
        }
    }
}