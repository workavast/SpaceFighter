using Configs.Missions;
using UnityEngine;
using Zenject;

public class SelectedMissionData : MonoBehaviour
{
    [Inject] private MissionsConfig _missionsConfig;

    private static MissionConfig _missionConfig;
    private static int _missionIndex;
        
    public void SetMissionData(int missionIndex)
    {
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
            
        return _missionIndex;
    }
}