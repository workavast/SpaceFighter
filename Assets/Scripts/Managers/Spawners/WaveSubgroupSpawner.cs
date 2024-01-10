using System;
using Configs.Missions;
using MissionsDataConfigsSystem;
using SomeStorages;
using TimerExtension;

namespace Managers
{
    public class WaveSubgroupSpawner : IHandleUpdate
    {
        private readonly SomeStorageInt _enemiesCounter;
        private readonly SomeStorageFloat _spawnPause;
        private readonly Timer _timer;
        private readonly EnemyGroupConfig _enemyGroup;

        public event Action OnEndSpawn;
        private event Action<float> OnHandleUpdate;

        public event Action<int, EnemyGroupConfig> EnemyInstanceDelegate;
        
        public WaveSubgroupSpawner(EnemyGroupConfig enemyGroup, Action<int, EnemyGroupConfig> enemyInstanceDelegate)
        {
            _enemyGroup = enemyGroup;
            EnemyInstanceDelegate = enemyInstanceDelegate;
            _enemiesCounter = new SomeStorageInt(_enemyGroup.enemySubgroup.Count);
            
            _timer = new Timer(_enemyGroup.EnemiesTimePause);
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
            EnemyInstanceDelegate?.Invoke(_enemiesCounter.CurrentValue, _enemyGroup);
                
            _enemiesCounter.ChangeCurrentValue(1);
            if (_enemiesCounter.IsFull)
            {
                OnEndSpawn?.Invoke();
                OnHandleUpdate = null;
            }
        }
    }
}