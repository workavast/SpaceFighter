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
        [Range(0, 30)] public float timePause;
        [Range(0, 10)] public int subgroupsCount;
        [Range(0, 20)] public float distanceBetweenSubgroups;
        [Range(0, 10)] public float moveSpeed;
        [Range(0, 20)] public float distanceBetweenEnemies;
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

    private void Update()
    {
        IReadOnlyDictionary<EnemySpaceshipsEnum, IReadOnlyList<EnemySpaceShip>> buse = _spaceShipsPool.BusyElements;

        bool buseEmpty = true;
        foreach (var pair in buse)
        {
            if (pair.Value.Count > 0)
            {
                buseEmpty = false;
                break;
            }
        }
        
        if (buseEmpty) CallWave();
    }

    private int _nextWave = 0;
    private void CallWave()
    {
        for (int i = 0; i < enemyWaves[_nextWave].enemyGroups.Count; i++)
            StartCoroutine(SpawnShips(_nextWave, i));
        
        _nextWave++;
    }

    IEnumerator SpawnShips(int waveIndex, int groupIndex)
    {
        EnemyGroup enemyGroup = enemyWaves[waveIndex].enemyGroups[groupIndex];
        
        if(enemyGroup.timePause > 0) yield return new WaitForSeconds(enemyGroup.timePause);

        float timePauseBetweenEnemies = enemyGroup.distanceBetweenEnemies / enemyGroup.moveSpeed;
        float timePauseBetweenSubgroup = enemyGroup.distanceBetweenSubgroups / enemyGroup.moveSpeed;
        
        for (int i = 0; i < enemyGroup.subgroupsCount; i++)
        {
            for (int j = 0; j < enemyGroup.enemySubgroup.Count; j++)
            {
                if(_spaceShipsPool.ExtractElement(enemyGroup.enemySubgroup[j], out EnemySpaceShip enemySpaceShip))
                {
                    enemySpaceShip.ChangePathWay(enemyGroup.path);
                    enemySpaceShip.SetMoveSpeed(enemyGroup.moveSpeed);
                }
                yield return new WaitForSeconds(timePauseBetweenEnemies);
            }
            yield return new WaitForSeconds(timePauseBetweenSubgroup);
        }
    }
    
    private EnemySpaceShip EnemySpaceShipInstantiate(EnemySpaceshipsEnum id)
    {
        return EnemiesFactory.CreateEnemySpaceship(id, transform);
    }
}