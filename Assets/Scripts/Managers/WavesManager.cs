﻿using System;
using EventBusExtension;
using GameCycle;
using SomeStorages;
using UnityEngine;
using WaveSpawnerSystem;
using Zenject;

namespace Managers
{
    public class WavesManager : GameCycleManager
    {
        protected override GameCycleState GameCycleState => GameCycleState.Gameplay;

        private SomeStorageInt _wavesCounter;
        private WaveSpawner _waveSpawner;
        
        [Inject] private EnemySpaceshipsManager _enemySpaceshipsManager;
        [Inject] private SelectedMissionData _selectedMissionData;
        [Inject] private EventBus _eventBus;
        
        public IReadOnlySomeStorage<int> WavesCounter => _wavesCounter;

        public event Action OnWavesEnd;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _waveSpawner = new WaveSpawner(_eventBus);
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
                OnWavesEnd?.Invoke();
            }
            else
            {
                _waveSpawner.CallWave(_selectedMissionData.TakeMissionData().enemyWaves[_wavesCounter.CurrentValue]);
                _wavesCounter.ChangeCurrentValue(1);
            }
        }
        
        private void WaveSpawnEnd()
        {
            if (_enemySpaceshipsManager.ActiveEnemiesCount <= 0)
                TryCallWave();
            else
                _enemySpaceshipsManager.OnAllEnemiesGone += TryCallWave;
        }
    }
}