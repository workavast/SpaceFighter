using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathCreation;
using UnityEngine.Serialization;

public class EnemySpaceshipsSpawner : MonoBehaviour
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
    
    [SerializeField] private List<EnemyWave> enemyWaves;

    private Pool<EnemySpaceshipBase, EnemySpaceshipsEnum> _spaceShipsPool;

    private Dictionary<EnemySpaceshipsEnum, GameObject> _spaceshipsParents = new Dictionary<EnemySpaceshipsEnum, GameObject>();

    private void Awake()
    {
        _spaceShipsPool = new Pool<EnemySpaceshipBase, EnemySpaceshipsEnum>(EnemySpaceShipInstantiate);
        
        foreach (var enemyShipId in Enum.GetValues(typeof(EnemySpaceshipsEnum)).Cast<EnemySpaceshipsEnum>())
        {
            GameObject parent = new GameObject(enemyShipId.ToString()) { transform = { parent = transform } };
            _spaceshipsParents.Add(enemyShipId, parent);
        }
    }

    void Start()
    {
        CallWave();
    }

    private void Update()
    {
        IReadOnlyDictionary<EnemySpaceshipsEnum, IReadOnlyList<EnemySpaceshipBase>> buse = _spaceShipsPool.BusyElements;

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
        
        IReadOnlyList<IReadOnlyList<IHandleUpdate>> list = _spaceShipsPool.BusyElementsValues;
        for (int i = 0; i < list.Count(); i++)
        for (int j = 0; j < list[i].Count; j++)
            list[i][j].HandleUpdate();
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
                if(_spaceShipsPool.ExtractElement(enemyGroup.enemySubgroup[j], out EnemySpaceshipBase enemySpaceShip))
                {
                    enemySpaceShip.ChangePathWay(enemyGroup.path);
                    enemySpaceShip.SetMoveSpeed(enemyGroup.moveSpeed);
                }
                yield return new WaitForSeconds(timePauseBetweenEnemies);
            }
            yield return new WaitForSeconds(timePauseBetweenSubgroup);
        }
    }
    
    private EnemySpaceshipBase EnemySpaceShipInstantiate(EnemySpaceshipsEnum id)
    {
        return EnemySpaceshipsFactory.Create(id, _spaceshipsParents[id].transform).GetComponentInChildren<EnemySpaceshipBase>();
    }
}