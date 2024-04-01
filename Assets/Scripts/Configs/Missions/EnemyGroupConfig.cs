using System;
using System.Collections.Generic;
using PathCreation;
using SpaceShips.Enemies;
using UnityEngine;

namespace Configs.Missions
{
    [Serializable]
    public class EnemyGroupConfig
    {
        [field: SerializeField] [field: Range(0, 30)] public float StartTimePause { get; private set; }
        [field: SerializeField] [field: Range(0, 10)] public int SubgroupsCount { get; private set; }
        [field: SerializeField] [field: Range(0, 20)] public float DistanceBetweenSubgroups { get; private set; }
        [field: SerializeField] [field: Range(0, 10)] public float MoveSpeed { get; private set; }
        [field: SerializeField] [field: Range(0, 20)] public float DistanceBetweenEnemies { get; private set; }
        [field: SerializeField] public List<EnemySpaceshipType> EnemySubgroup { get; private set; }
        [field: Space]
        [field: SerializeField] public PathCreator Path { get; private set; }
        [field: SerializeField] public EnemyPathWayMoveType PathWayMoveType { get; private set; }
        [field: SerializeField] public EnemyRotationType RotationType { get; private set; }
        
        public float SubgroupsTimePause => DistanceBetweenSubgroups / MoveSpeed;
        public float EnemiesTimePause =>  DistanceBetweenEnemies / MoveSpeed;
    }
}