using System;
using EventBus;
using GameCycle;
using Managers.Spawners;
using MissionsDataConfigsSystem;
using SomeStorages;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class WavesManager : ManagerBase
    {
        protected override GameStatesType GameStatesType => GameStatesType.Gameplay;

        private SomeStorageInt _wavesCounter;
        private WaveSpawner _waveSpawner;
        
        [Inject] private EnemySpaceshipsManager _enemySpaceshipsManager;
        [Inject] private SelectedMissionData _selectedMissionData;
        [Inject] private MissionEventBus _missionEventBus;
        
        public IReadOnlySomeStorage<int> WavesCounter => _wavesCounter;

        public event Action OnWavesEnd;
        
        protected override void OnAwake()
        {
            _waveSpawner = new WaveSpawner(_missionEventBus.EventBus);
            _wavesCounter = new SomeStorageInt(_selectedMissionData.TakeMissionData().enemyWaves.Count);
            _waveSpawner.OnWaveSpawned += WaveSpawnEnd;
        }
        
        public override void GameCycleUpdate()
        {
            _waveSpawner.HandleUpdate(Time.deltaTime);
        }
        
        public void StartWaves() => TryCallWave();
        
        private void TryCallWave()
        {
            _enemySpaceshipsManager.OnAllEnemiesGone -= TryCallWave;

            if (_wavesCounter.IsFull)
            {
                Debug.Log("All waves completed");
                OnWavesEnd?.Invoke();
            }
            else
            {
                Debug.Log($"Current wave index: {_wavesCounter.CurrentValue}");
                _waveSpawner.CallWave(_selectedMissionData.TakeMissionData().enemyWaves[_wavesCounter.CurrentValue]);
                _wavesCounter.ChangeCurrentValue(1);
            }
        }
        
        private void WaveSpawnEnd()
        {
            Debug.Log("wave spawned");
            if (_enemySpaceshipsManager.ActiveEnemiesCount <= 0)
                TryCallWave();
            else
                _enemySpaceshipsManager.OnAllEnemiesGone += TryCallWave;
        }
    }
}