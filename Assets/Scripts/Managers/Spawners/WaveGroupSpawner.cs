using System;
using Configs.Missions;
using EventBus;
using SomeStorages;
using TimerExtension;

namespace Managers.Spawners
{
    public class WaveGroupSpawner : IHandleUpdate
    {
        private readonly EnemyGroupConfig _enemyGroupConfig;
        private readonly SomeStorageInt _subgroupsCounter;
        private readonly WaveSubgroupSpawner _waveSubgroupSpawner;
        private readonly Timer _timer;

        public event Action OnGroupSpawnEnd;
        private event Action<float> OnHandleUpdate;
        
        public WaveGroupSpawner(EnemyGroupConfig enemyGroupConfig, EventBusExtension.EventBus eventBus)
        {
            _enemyGroupConfig = enemyGroupConfig;
            _subgroupsCounter = new SomeStorageInt(enemyGroupConfig.subgroupsCount);

            _waveSubgroupSpawner = new WaveSubgroupSpawner(enemyGroupConfig, eventBus);
            _waveSubgroupSpawner.OnEndSpawn += SubgroupSpawned;
            
            _timer = new Timer(_enemyGroupConfig.StartTimePause);
            _timer.OnTimerEnd += StartPauseEnd;

            OnHandleUpdate += _timer.Tick;
        }

        public void HandleUpdate(float time) => OnHandleUpdate?.Invoke(time);
        
        private void StartPauseEnd()
        {
            _timer.OnTimerEnd -= StartPauseEnd;
            _timer.SetTimer(_enemyGroupConfig.SubgroupsTimePause);
            _timer.OnTimerEnd += SpawnSubgroup;
            
            SpawnSubgroup();
        }

        private void SpawnSubgroup()
        {
            OnHandleUpdate -= _timer.Tick;
            
            _waveSubgroupSpawner.Reset();
            OnHandleUpdate += _waveSubgroupSpawner.HandleUpdate;
        }

        private void WaitPauseBetweenSubgroups()
        {
            _timer.Reset();
            OnHandleUpdate += _timer.Tick;
            OnHandleUpdate -= _waveSubgroupSpawner.HandleUpdate;
        }

        private void SubgroupSpawned()
        {
            _subgroupsCounter.ChangeCurrentValue(1);

            if (_subgroupsCounter.IsFull)
            {
                OnGroupSpawnEnd?.Invoke();
                OnHandleUpdate = null;
            }
            else
            {
                WaitPauseBetweenSubgroups();
            }
        }
    }
}