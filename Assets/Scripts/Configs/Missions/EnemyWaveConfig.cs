using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Configs.Missions
{
    [CreateAssetMenu(fileName = "EnemyWaveConfig", menuName = "SO/EnemyWaveConfig")]
    public class EnemyWaveConfig : ScriptableObject
    {
        [field: SerializeField] public List<EnemyGroupConfig> GroupsConfigs { get; private set; }
        
        public int TakeEnemiesCount() => GroupsConfigs.Sum(group => group.EnemySubgroup.Count * group.SubgroupsCount);
    }
}