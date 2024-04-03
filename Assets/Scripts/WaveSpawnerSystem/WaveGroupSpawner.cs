using System;
using Configs.Missions;
using Factories;
using SomeStorages;
using TimerExtension;

namespace WaveSpawnerSystem
{
    public class WaveGroupSpawner
    {
        private readonly WaveSubgroupSpawner _waveSubgroupSpawner;
        private readonly EnemyGroupConfig _enemyGroupConfig;
        private readonly SomeStorageInt _subgroupsCounter;
        private readonly Timer _timer;

        public event Action OnGroupSpawnEnd;
        private event Action<float> OnHandleUpdate;
        
        public WaveGroupSpawner(EnemyGroupConfig enemyGroupConfig, EnemySpaceshipsFactory enemySpaceshipsFactory)
        {
            _enemyGroupConfig = enemyGroupConfig;
            _subgroupsCounter = new SomeStorageInt(enemyGroupConfig.SubgroupsCount);

            _waveSubgroupSpawner = new WaveSubgroupSpawner(enemyGroupConfig, enemySpaceshipsFactory);
            _waveSubgroupSpawner.OnEndSpawn += SubgroupSpawned;
            
            _timer = new Timer(_enemyGroupConfig.StartTimePause);
            _timer.OnTimerEnd += StartPauseEnd;

            OnHandleUpdate += _timer.Tick;
        }

        public void HandleUpdate(float time) 
            => OnHandleUpdate?.Invoke(time);
        
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