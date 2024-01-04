using System;
using System.Collections.Generic;
using UnityEngine;

namespace MissionsDataConfigsSystem
{
    [CreateAssetMenu(fileName = "MissionsConfig", menuName = "SO/MissionsConfig")]
    public class MissionsConfig : ScriptableObject
    {
        [SerializeField] private List<MissionConfig> missions;

        public MissionConfig GetMissionData(int missionNum)
        {
            if (missionNum < 0 || missionNum > missions.Count) throw new Exception("Incorrect mission num");
            
            Debug.Log(missionNum);
            return missions[missionNum];
        }
    }
}