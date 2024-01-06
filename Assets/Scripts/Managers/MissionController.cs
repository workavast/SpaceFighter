using System;
using GameCycle;
using MissionsDataConfigsSystem;
using SomeStorages;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class MissionController : MonoBehaviour
    {
        private readonly SomeStorageInt _wavesCounter = new();
        private readonly SomeStorageInt _destroyedEnemiesCounter = new();
        private readonly SomeStorageInt _escapedEnemiesCounter = new();

        public IReadOnlySomeStorage<int> WavesCounter => _wavesCounter;
        public IReadOnlySomeStorage<int> DestroyedEnemiesCounter => _destroyedEnemiesCounter;
        public IReadOnlySomeStorage<int> EscapedShipsEnemiesCounter => _escapedEnemiesCounter;
        
        [Inject] private IGameCycleManager _gameCycleManager;
        [Inject] private EnemySpaceshipsManager _enemySpaceshipsManager;
        [Inject] private WavesManager _wavesManager;

        private MissionConfig _missionConfig;

        public event Action OmMissionCompleted;
        
        private void Start()
        {
            _missionConfig = SelectedMissionData.MissionConfig;
            
            _wavesCounter.SetMaxValue(_missionConfig.enemyWaves.Count);
            _destroyedEnemiesCounter.SetMaxValue(_missionConfig.TakeEnemiesCount());
            _escapedEnemiesCounter.SetMaxValue(_missionConfig.TakeEnemiesCount());
            
            _wavesManager.OnWaveSpawned += WaveSpawnEnd;

            _enemySpaceshipsManager.OnEnemyDead += IncreaseDestroyedEnemiesCount;
            _enemySpaceshipsManager.OnEnemyEscape += IncreaseEscapedEnemiesCount;
            
            TryCallWave();
        }

        private void TryCallWave()
        {
            _enemySpaceshipsManager.OnAllEnemiesGone -= TryCallWave;

            if (_wavesCounter.IsFull)
            {
                Debug.Log("Mission Completed");
                LevelMoneyStarsCounter.ApplyValue();
                OmMissionCompleted?.Invoke();
            }
            else
            {
                Debug.Log($"Current wave index: {_wavesCounter.CurrentValue}");
                _wavesManager.CallWave(_missionConfig.enemyWaves[_wavesCounter.CurrentValue]);
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

        private void IncreaseDestroyedEnemiesCount() => _destroyedEnemiesCounter.ChangeCurrentValue(1);
        private void IncreaseEscapedEnemiesCount() => _escapedEnemiesCounter.ChangeCurrentValue(1);
    }
}