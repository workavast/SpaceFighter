using Configs.Missions;
using UnityEngine;
using Zenject;

namespace MissionsDataConfigsSystem
{
    public class SelectedMissionData : MonoBehaviour
    {
        [Inject] private MissionsConfig _missionsConfig;

        private static MissionConfig _missionConfig;
        private static int _missionIndex;
        
        public void SetMissionData(int missionIndex)
        {
            Debug.LogWarning($"Mission index for loading {missionIndex}");
            _missionIndex = missionIndex;
            _missionConfig = _missionsConfig.GetMissionData(_missionIndex);
        }

        public MissionConfig TakeMissionData()
        {
            if (_missionConfig is null)
            {
                Debug.LogWarning($"mission config is null");
                SetMissionData(0);
            }
            
            return _missionConfig;
        }
        
        public int TakeMissionIndex()
        {
            if (_missionConfig is null)
            {
                Debug.LogWarning($"mission config is null");
                return 0;
            }
            
            Debug.LogWarning($"mission index for return {_missionIndex}");
            return _missionIndex;
        }
    }
}