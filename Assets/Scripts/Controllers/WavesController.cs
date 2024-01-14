using System;
using Managers;
using MissionsDataConfigsSystem;
using SomeStorages;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class WavesController : ControllerBase
    {
        private SomeStorageInt _wavesCounter;
        
        [Inject] private EnemySpaceshipsManager _enemySpaceshipsManager;
        [Inject] private WavesManager _wavesManager;
        [Inject] private SelectedMissionData _selectedMissionData;

        public IReadOnlySomeStorage<int> WavesCounter => _wavesCounter;

        public event Action OnWavesEnd;
        
        public void Awake()
        {
            _wavesCounter = new SomeStorageInt(_selectedMissionData.TakeMissionData().enemyWaves.Count);
            _wavesManager.OnWaveSpawned += WaveSpawnEnd;
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
                _wavesManager.CallWave(_selectedMissionData.TakeMissionData().enemyWaves[_wavesCounter.CurrentValue]);
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