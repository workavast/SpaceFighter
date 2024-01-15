using System;
using System.Collections.Generic;
using Configs.Missions;
using EventBus;
using SomeStorages;

namespace Managers.Spawners
{
    public class WaveSpawner : IHandleUpdate
    {
        private readonly EventBusExtension.EventBus _eventBus;

        private SomeStorageInt _spawnedGroupsCount;
        private List<WaveGroupSpawner> _waveGroupsSpawners;
        
        private bool _invokeWave;

        public event Action OnWaveSpawned;

        public WaveSpawner(EventBusExtension.EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void HandleUpdate(float time)
        {
            if(!_invokeWave) return;

            foreach (var group in _waveGroupsSpawners)
                group.HandleUpdate(time);
        }
        
        public void CallWave(EnemyWaveConfig waveConfig)
        {
            _invokeWave = true;
            
            _waveGroupsSpawners = new List<WaveGroupSpawner>();
            foreach (var groupConfig in waveConfig.GroupsConfigs)
            {
                var waveGroupSpawner = new WaveGroupSpawner(groupConfig, _eventBus);
                waveGroupSpawner.OnGroupSpawnEnd += GroupSpawnEnd;
                _waveGroupsSpawners.Add(waveGroupSpawner);
            }
            
            _spawnedGroupsCount = new SomeStorageInt(_waveGroupsSpawners.Count);
        }

        private void GroupSpawnEnd()
        {
            _spawnedGroupsCount.ChangeCurrentValue(1);
            if(_spawnedGroupsCount.IsFull)
                WaveSpawnEnd();
        }
        
        private void WaveSpawnEnd()
        {
            _waveGroupsSpawners = new List<WaveGroupSpawner>();
            _invokeWave = false;

            OnWaveSpawned?.Invoke(); 
        }
    }
}