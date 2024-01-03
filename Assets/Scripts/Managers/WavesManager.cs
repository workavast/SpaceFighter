using System;
using System.Collections.Generic;
using GameCycle;
using MissionsDataConfigsSystem;
using SomeStorages;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class WavesManager : ManagerBase
    {
        protected override GameStatesType GameStatesType => GameStatesType.Gameplay;
        
        [Inject] private EnemySpaceshipsManager _enemySpaceshipsManager;

        private SomeStorageInt _spawnedGroupsCount;
        private List<WaveGroupSpawner> _waveGroupsSpawners;
        
        public event Action OnWaveSpawned;
        
        public override void GameCycleUpdate()
        {
            var time = Time.deltaTime;
            foreach (var group in _waveGroupsSpawners)
                group.HandleUpdate(time);
        }
        
        public void CallWave(EnemyWaveConfig waveConfig)
        {
            _waveGroupsSpawners = new List<WaveGroupSpawner>();
            foreach (var groupConfig in waveConfig.GroupsConfigs)
            {
                var waveGroupSpawner = new WaveGroupSpawner(groupConfig, EnemyInstanceDelegate);
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
            Debug.Log("Wave spawned");
            _waveGroupsSpawners = new List<WaveGroupSpawner>();
            OnWaveSpawned?.Invoke();
        }
        
        private void EnemyInstanceDelegate(int enemyIndex, EnemyGroupConfig enemyGroupConfig)
        {
            _enemySpaceshipsManager.SpawnEnemy(enemyGroupConfig.enemySubgroup[enemyIndex], enemyGroupConfig);
        }
    }
}