using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

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

    [SerializeField] private List<MyStruct> list;

    [SerializeField] private DictionaryInspector<SpaceShips, GameObject> dictionaryInspector;
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
        for (int i = 0; i < list[index].groupsCount; i++)
        {
            for (int j = 0; j < list[index].enemyGroup.Count; j++)
            {
                if(_spaceShipsPool.ExtractElement(list[index].enemyGroup[j], out EnemySpaceShip enemySpaceShip))
                {
                    enemySpaceShip.ChangePathWay(list[index].path);
                }
                yield return new WaitForSeconds(list[index].timePauseInGroup);
            }

            yield return new WaitForSeconds(list[index].timePauseBetweenGroups);
        }
    }
    void Update()
    {
        
    }
    
    private EnemySpaceShip EnemySpaceShipInstantiate(SpaceShips id)
    {
        return Instantiate(dictionaryInspector[id], transform).GetComponentInChildren<EnemySpaceShip>();
    }
}
