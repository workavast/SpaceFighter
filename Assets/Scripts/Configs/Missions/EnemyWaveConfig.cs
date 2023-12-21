using System.Collections.Generic;
using UnityEngine;

namespace MissionsDataConfigsSystem
{
    [CreateAssetMenu(fileName = "EnemyWaveConfig", menuName = "SO/EnemyWaveConfig")]
    public class EnemyWaveConfig : ScriptableObject
    {
        [field: SerializeField] public List<EnemyGroupConfig> GroupsConfigs { get; private set; }
    }
}