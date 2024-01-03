using MissionsDataConfigsSystem;
using SomeStorages;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class MissionController : MonoBehaviour
    {
        [Inject] private EnemySpaceshipsManager _enemySpaceshipsManager;
        [Inject] private WavesManager _wavesManager;

        private SomeStorageInt _wavesCounter;
        private MissionConfig _missionConfig;
        
        private void Start()
        {
            _missionConfig = SelectedMissionData.MissionConfig;
            _wavesCounter = new SomeStorageInt(_missionConfig.enemyWaves.Count);

            _wavesManager.OnWaveSpawned += WaveSpawnEnd;
            
            TryCallWave();
        }

        private void TryCallWave()
        {
            _enemySpaceshipsManager.OnAllEnemiesGone -= TryCallWave;

            if (_wavesCounter.IsFull)
            {
                Debug.Log("Mission Completed");
                LevelMoneyStarsCounter.ApplyValue();    
            }
            else
            {
                Debug.Log($"Current wave index: {_wavesCounter.CurrentValue}");
                _wavesManager.CallWave(_missionConfig.enemyWaves[_wavesCounter.CurrentValue]);
            }
        }

        private void WaveSpawnEnd()
        {
            Debug.Log("wave spawned");
            _wavesCounter.ChangeCurrentValue(1);
            var enemiesAliveCount = _enemySpaceshipsManager.ActiveEnemiesCount;

            if (enemiesAliveCount <= 0)
            {
                TryCallWave();
            }
            else
            {
                _enemySpaceshipsManager.OnAllEnemiesGone += TryCallWave;
            }
        }
    }
}