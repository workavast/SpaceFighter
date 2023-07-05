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
        public List<EnemySpaceshipsEnum> enemySubgroup;
        [Range(0, 10)] public int subgroupsCount;
        [Range(0, 10)] public float timePauseInSubgroup;
        [Range(0, 10)] public float timePauseBetweenSubgroup;
        public PathCreator path;
    }

    [System.Serializable]
    private struct EnemyWave
    {
        [Range(0, 10)] public float timePause;
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
        for (int i = 0; i < enemyWaves[waveIndex].enemyGroups[groupIndex].subgroupsCount; i++)
        {
            for (int j = 0; j < enemyWaves[waveIndex].enemyGroups[groupIndex].enemySubgroup.Count; j++)
            {
                if(_spaceShipsPool.ExtractElement(enemyWaves[waveIndex].enemyGroups[groupIndex].enemySubgroup[j], out EnemySpaceShip enemySpaceShip))
                {
                    enemySpaceShip.ChangePathWay(enemyWaves[waveIndex].enemyGroups[groupIndex].path);
                }
                yield return new WaitForSeconds(enemyWaves[waveIndex].enemyGroups[groupIndex].timePauseInSubgroup);
            }

            yield return new WaitForSeconds(enemyWaves[waveIndex].enemyGroups[groupIndex].timePauseBetweenSubgroup);
        }
    }
    
    private EnemySpaceShip EnemySpaceShipInstantiate(EnemySpaceshipsEnum id)
    {
        return EnemiesFactory.CreateEnemySpaceship(id, transform);
    }
}
