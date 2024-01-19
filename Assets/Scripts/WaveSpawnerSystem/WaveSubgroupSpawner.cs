using System;
using Configs.Missions;
using EventBus.Events;
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
        private readonly EventBusExtension.EventBus _eventBus;

        public event Action OnEndSpawn;
        private event Action<float> OnHandleUpdate;
        
        public WaveSubgroupSpawner(EnemyGroupConfig enemyGroupConfig, EventBusExtension.EventBus eventBus)
        {
            _enemyGroupConfig = enemyGroupConfig;
            _enemiesCounter = new SomeStorageInt(_enemyGroupConfig.enemySubgroup.Count);
            _eventBus = eventBus;
            
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
            _eventBus.Invoke(new SpawnEnemy(_enemiesCounter.CurrentValue, _enemyGroupConfig));
            
            _enemiesCounter.ChangeCurrentValue(1);
            if (_enemiesCounter.IsFull)
            {
                OnEndSpawn?.Invoke();
                OnHandleUpdate = null;
            }
        }
    }
}