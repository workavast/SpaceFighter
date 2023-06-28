using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.Serialization;

public class EnemiesSpawner : MonoBehaviour
{
    [System.Serializable]
    private struct MyStruct
    {
        public List<SpaceShips> enemyGroup;
        public int groupsCount;
        public float timePauseInGroup;
        public float timePauseBetweenGroups;
        public PathCreator path;
    }

    [SerializeField] private List<MyStruct> enemiesWaves;

    [SerializeField] private DictionaryInspector<SpaceShips, GameObject> enemiesPrefabs;
    private Pool<EnemySpaceShip, SpaceShips> _spaceShipsPool;

    private void Awake()
    {
        _spaceShipsPool = new Pool<EnemySpaceShip, SpaceShips>(EnemySpaceShipInstantiate);
    }

    void Start()
    {
        StartCoroutine(SpawnShips(0));
    }

    IEnumerator SpawnShips(int index)
    {
        for (int i = 0; i < enemiesWaves[index].groupsCount; i++)
        {
            for (int j = 0; j < enemiesWaves[index].enemyGroup.Count; j++)
            {
                if(_spaceShipsPool.ExtractElement(enemiesWaves[index].enemyGroup[j], out EnemySpaceShip enemySpaceShip))
                {
                    enemySpaceShip.ChangePathWay(enemiesWaves[index].path);
                }
                yield return new WaitForSeconds(enemiesWaves[index].timePauseInGroup);
            }

            yield return new WaitForSeconds(enemiesWaves[index].timePauseBetweenGroups);
        }
    }
    void Update()
    {
        
    }
    
    private EnemySpaceShip EnemySpaceShipInstantiate(SpaceShips id)
    {
        return Instantiate(enemiesPrefabs[id], transform).GetComponentInChildren<EnemySpaceShip>();
    }
}
