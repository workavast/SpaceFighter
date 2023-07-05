using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.Serialization;

public class EnemiesSpawner : MonoBehaviour
{
    [System.Serializable]
    private struct EnemyGroup
    {
        [Range(0, 10)] public float timePause;
        [Range(0, 10)] public int subgroupsCount;
        [Range(0, 10)] public float timePauseInSubgroup;
        [Range(0, 10)] public float timePauseBetweenSubgroup;
        public PathCreator path;
        public List<EnemySpaceshipsEnum> enemySubgroup;
    }

    [System.Serializable]
    private struct EnemyWave
    {
        public List<EnemyGroup> enemyGroups;
    }
    
    [FormerlySerializedAs("enemiesWaves")] [SerializeField] private List<EnemyWave> enemyWaves;

    private Pool<EnemySpaceShip, EnemySpaceshipsEnum> _spaceShipsPool;

    private void Awake()
    {
        _spaceShipsPool = new Pool<EnemySpaceShip, EnemySpaceshipsEnum>(EnemySpaceShipInstantiate);
    }

    void Start()
    {
        CallWave();
    }

    private int _nextWave = 0;
    private void CallWave()
    {
        for (int i = 0; i < enemyWaves[_nextWave].enemyGroups.Count; i++)
        {
            StartCoroutine(SpawnShips(_nextWave, i));
        }
        
        _nextWave++;
    }

    IEnumerator SpawnShips(int waveIndex, int groupIndex)
    {
        EnemyGroup enemyGroup = enemyWaves[waveIndex].enemyGroups[groupIndex];
        
        yield return new WaitForSeconds(enemyGroup.timePause);
        
        for (int i = 0; i < enemyGroup.subgroupsCount; i++)
        {
            for (int j = 0; j < enemyGroup.enemySubgroup.Count; j++)
            {
                if(_spaceShipsPool.ExtractElement(enemyGroup.enemySubgroup[j], out EnemySpaceShip enemySpaceShip))
                {
                    enemySpaceShip.ChangePathWay(enemyGroup.path);
                }
                yield return new WaitForSeconds(enemyGroup.timePauseInSubgroup);
            }
            yield return new WaitForSeconds(enemyGroup.timePauseBetweenSubgroup);
        }
    }
    
    private EnemySpaceShip EnemySpaceShipInstantiate(EnemySpaceshipsEnum id)
    {
        return EnemiesFactory.CreateEnemySpaceship(id, transform);
    }
}