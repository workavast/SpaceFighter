using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathCreation;
using UnityEngine.Serialization;
using PoolSystem;

public enum EnemyRotationType
{
    Forward,
    PlayerTarget,
    PathWayRotation
}

public enum PathWayMoveType
{
    Loop,
    OnEndRemove,
    OnEndStop
}

public class EnemySpaceshipsSpawner : MonoBehaviour
{
    [Serializable]
    private struct EnemyGroup
    {
        [Range(0, 30)] public float timePause;
        [Range(0, 10)] public int subgroupsCount;
        [Range(0, 20)] public float distanceBetweenSubgroups;
        [Range(0, 10)] public float moveSpeed;
        [Range(0, 20)] public float distanceBetweenEnemies;
        public List<EnemySpaceshipsEnum> enemySubgroup;
        
        [Space]
        public PathCreator path;
        public EndOfPathInstruction endOfPathInstruction;
        public PathWayMoveType moveType;
        public EnemyRotationType rotationType;
        [Space]
        public bool accelerated;
        public AnimationCurve acceleration;
    }

    [Serializable]
    private struct EnemyWave
    {
        public List<EnemyGroup> enemyGroups;
    }
    
    [SerializeField] private List<EnemyWave> enemyWaves;

    private Pool<EnemySpaceshipBase, EnemySpaceshipsEnum> _spaceShipsPool;

    private Dictionary<EnemySpaceshipsEnum, GameObject> _spaceshipsParents = new Dictionary<EnemySpaceshipsEnum, GameObject>();

    private int _currentGroupsCount = 0;
    private int _nextWave = 0;

    private bool _levelCompleted = false;
    
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


        if (buseEmpty && _currentGroupsCount <= 0 && _nextWave >= enemyWaves.Count && !_levelCompleted)
        {
            _levelCompleted = true;
            Debug.Log("level completed");
            LevelMoneyStarsCounter.ApplyValue();    
        }
        
        if (buseEmpty && _currentGroupsCount <= 0 && _nextWave < enemyWaves.Count) CallWave();
        
        IReadOnlyList<IReadOnlyList<IHandleUpdate>> list = _spaceShipsPool.BusyElementsValues;
        for (int i = 0; i < list.Count(); i++)
        for (int j = 0; j < list[i].Count; j++)
            list[i][j].HandleUpdate();
    }

    private void CallWave()
    {
        for (int i = 0; i < enemyWaves[_nextWave].enemyGroups.Count; i++)
            StartCoroutine(SpawnShips(_nextWave, i));
        
        _nextWave++;
    }
    
    IEnumerator SpawnShips(int waveIndex, int groupIndex)
    {
        _currentGroupsCount++;
        
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
                    enemySpaceShip.SetWaveData(enemyGroup.moveSpeed,enemyGroup.path,enemyGroup.endOfPathInstruction,enemyGroup.moveType,enemyGroup.rotationType,enemyGroup.accelerated,enemyGroup.acceleration);
                }
                yield return new WaitForSeconds(timePauseBetweenEnemies);
            }
            yield return new WaitForSeconds(timePauseBetweenSubgroup);
        }

        _currentGroupsCount--;
    }
    
    private EnemySpaceshipBase EnemySpaceShipInstantiate(EnemySpaceshipsEnum id)
    {
        return EnemySpaceshipsFactory.Create(id, _spaceshipsParents[id].transform).GetComponentInChildren<EnemySpaceshipBase>();
    }
}