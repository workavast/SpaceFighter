using System;
using EventBusExtension;
using Events;
using MissionsDataConfigsSystem;
using SomeStorages;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class MissionController : MonoBehaviour, IEventReceiver<EnemyStartDie>
    {
        public ReceiverIdentifier ReceiverIdentifier { get; } = new();

        private readonly SomeStorageInt _wavesCounter = new();
        private readonly SomeStorageInt _destroyedEnemiesCounter = new();

        public IReadOnlySomeStorage<int> WavesCounter => _wavesCounter;
        public IReadOnlySomeStorage<int> DestroyedEnemiesCounter => _destroyedEnemiesCounter;
        
        [Inject] private EnemySpaceshipsManager _enemySpaceshipsManager;
        [Inject] private WavesManager _wavesManager;
        [Inject] private MissionEventBus _eventBus;

        private MissionConfig _missionConfig;

        public event Action OmMissionCompleted;
        
        private void Start()
        {
            _missionConfig = SelectedMissionData.MissionConfig;
            
            _wavesCounter.SetMaxValue(_missionConfig.enemyWaves.Count);
            _destroyedEnemiesCounter.SetMaxValue(_missionConfig.TakeEnemiesCount());
            
            _wavesManager.OnWaveSpawned += WaveSpawnEnd;

            _eventBus.Subscribe<EnemyStartDie>(this);
            
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
        
        public void OnEvent(EnemyStartDie ev) => _destroyedEnemiesCounter.ChangeCurrentValue(1);

        private void OnDestroy()
        {
            _eventBus.UnSubscribe<EnemyStartDie>(this);
        }
    }
}