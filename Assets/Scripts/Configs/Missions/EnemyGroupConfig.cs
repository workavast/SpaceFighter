using System.Collections.Generic;
using PathCreation;
using UnityEngine;

namespace Configs.Missions
{
    [CreateAssetMenu(fileName = "EnemyGroupConfig", menuName = "SO/EnemyGroupConfig")]
    public class EnemyGroupConfig : ScriptableObject
    {
        [field: SerializeField] [field: Range(0, 30)] public float StartTimePause { get; private set; }
        [field: SerializeField] [field: Range(0, 10)] public int subgroupsCount { get; private set; }
        [field: SerializeField] [field: Range(0, 20)] public float distanceBetweenSubgroups { get; private set; }
        [field: SerializeField] [field: Range(0, 10)] public float moveSpeed { get; private set; }
        [field: SerializeField] [field: Range(0, 20)] public float distanceBetweenEnemies { get; private set; }
        [field: SerializeField] public List<EnemySpaceshipType> enemySubgroup { get; private set; }
        
        [field: Space]
        [field: SerializeField] public PathCreator path { get; private set; }
        [field: SerializeField] public EndOfPathInstruction endOfPathInstruction { get; private set; }
        [field: SerializeField] public EnemyPathWayMoveType pathWayMoveType { get; private set; }
        [field: SerializeField] public EnemyRotationType rotationType { get; private set; }
        [field: Space]
        [field: SerializeField] public bool accelerated { get; private set; }
        [field: SerializeField] public AnimationCurve acceleration { get; private set; }
        
        public float SubgroupsTimePause => distanceBetweenSubgroups / moveSpeed;
        public float EnemiesTimePause =>  distanceBetweenEnemies / moveSpeed;
    }
}