using UnityEngine;
using Zenject;

namespace MissionsDataConfigsSystem
{
    public class SelectedMissionData : MonoBehaviour
    {
        [Inject] private MissionsConfig _missionsConfig;
        private static SelectedMissionData _instance;

        private MissionConfig _missionConfig;
        public static MissionConfig MissionConfig => _instance._missionConfig;

        private void Awake()
        {
            if (_instance)
            {
                Destroy(this);
                return;
            }

            _instance = this;

            _missionConfig = null;

            DontDestroyOnLoad(this);
            
            SetMissionData(0);
        }

        public static void SetMissionData(int missionNum)
        {
            _instance._missionConfig = _instance._missionsConfig.GetMissionData(missionNum);
        }
    }
}