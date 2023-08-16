using System;
using System.Collections.Generic;
using UnityEngine;

namespace MissionsDataConfigsSystem
{
    [CreateAssetMenu(fileName = "MissionsDataConfig", menuName = "SO/MissionsDataConfig")]
    public class MissionsDataConfig : ScriptableObject
    {
        [SerializeField] private List<EnemyWavesConfig> missionsData;

        public EnemyWavesConfig GetMissionData(int missionNum)
        {
            if (missionNum < 0 || missionNum > missionsData.Count) throw new Exception("Incorrect mission num");

            return missionsData[missionNum];
        }
    }
}