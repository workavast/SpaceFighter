using Configs.Missions;
using UnityEngine;
using Zenject;

namespace MissionsDataConfigsSystem
{
    public class SelectedMissionData : MonoBehaviour
    {
        [Inject] private MissionsConfig _missionsConfig;

        private static MissionConfig _missionConfig;
        
        public void SetMissionData(int missionNum)
        {
            _missionConfig = _missionsConfig.GetMissionData(missionNum);
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
    }
}