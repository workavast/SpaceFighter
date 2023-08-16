using UnityEngine;
using Zenject;

namespace MissionsDataConfigsSystem
{
    public class SelectedMissionData : MonoBehaviour
    {
        [Inject] private MissionsDataConfig _missionsDataConfig;
        private static SelectedMissionData _instance;

        private EnemyWavesConfig _enemyWavesConfig;
        public static EnemyWavesConfig EnemyWavesConfig => _instance._enemyWavesConfig;

        private void Awake()
        {
            if (_instance)
            {
                Destroy(this);
                return;
            }

            _instance = this;

            _enemyWavesConfig = null;

            DontDestroyOnLoad(this);
            
            SetMissionData(0);
        }

        public static void SetMissionData(int missionNum)
        {
            _instance._enemyWavesConfig = _instance._missionsDataConfig.GetMissionData(missionNum);
        }
    }
}