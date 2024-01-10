using System.Collections.Generic;
using System.Linq;
using MissionsDataConfigsSystem;
using UnityEngine;

namespace Configs.Missions
{
    [CreateAssetMenu(fileName = "MissionConfig", menuName = "SO/MissionConfig")]
    public class MissionConfig : ScriptableObject
    {
        [field: SerializeField] public List<EnemyWaveConfig> enemyWaves { get; private set; }

        public int TakeEnemiesCount() => enemyWaves.Sum(wave => wave.TakeEnemiesCount());
    }
}
