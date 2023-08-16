using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using MissionsDataConfigsSystem;
using PoolSystem;

public class EnemySpaceshipsSpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyWaveConfig> enemyWaves;
    
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
        enemyWaves = SelectedMissionData.EnemyWavesConfig.enemyWaves;
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
        for (int i = 0; i < enemyWaves[_nextWave].enemyWave.Count; i++)
            StartCoroutine(SpawnShips(_nextWave, i));
        
        _nextWave++;
    }
    
    IEnumerator SpawnShips(int waveIndex, int groupIndex)
    {
        _currentGroupsCount++;
        
        EnemyGroupConfig enemyGroup = enemyWaves[waveIndex].enemyWave[groupIndex];
        
        if(enemyGroup.timePause > 0) yield return new WaitForSeconds(enemyGroup.timePause);

        float timePauseBetweenEnemies = enemyGroup.distanceBetweenEnemies / enemyGroup.moveSpeed;
        float timePauseBetweenSubgroup = enemyGroup.distanceBetweenSubgroups / enemyGroup.moveSpeed;
        for (int i = 0; i < enemyGroup.subgroupsCount; i++)
        {
            for (int j = 0; j < enemyGroup.enemySubgroup.Count; j++)
            {
                if(_spaceShipsPool.ExtractElement(enemyGroup.enemySubgroup[j], out EnemySpaceshipBase enemySpaceShip))
                {
                    enemySpaceShip.SetWaveData(enemyGroup.moveSpeed,enemyGroup.path,enemyGroup.endOfPathInstruction,enemyGroup.pathWayMoveType,enemyGroup.rotationType,enemyGroup.accelerated,enemyGroup.acceleration);
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