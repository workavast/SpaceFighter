using System;
using System.Collections.Generic;
using MissionsDataConfigsSystem;
using SomeStorages;
using UnityEngine;

namespace Managers
{
    public class EnemySpaceshipWaveSpawner : IHandleUpdate
    {
        private List<GroupCell> _groupCells = new List<GroupCell>();
        
        private List<EnemyWaveConfig> _enemyWaves;
        private int _waveIndex;
        private SomeStorageFloat _pauseTimer;

        public event Action OnWaveEnd;
        public event Action<EnemySpaceshipsEnum, EnemyGroupConfig> OnSpawnEnemy;
        private event Action<float> OnUpdate;
        
        public void HandleUpdate(float time)
        {
            OnUpdate?.Invoke(time);
        }

        public void CallWave(List<EnemyWaveConfig> enemyWaves, int waveIndex)
        {
            for (int i = 0; i < enemyWaves[waveIndex].GroupsConfigs.Count; i++)
            {
                EnemyGroupConfig enemyGroup = _enemyWaves[_waveIndex].GroupsConfigs[i];

                _groupCells.Add(new GroupCell(enemyGroup));
            }
            OnUpdate += SpawnShips;
        }

        private void StartPauseTimer(float deltaTime)
        {
            foreach (var group in _groupCells)
            {
                group.StartPauseTimer.ChangeCurrentValue(deltaTime);
                if (group.StartPauseTimer.IsFull)
                {

                }
            }
        }
        
        private void SpawnShips(float deltaTime)
        {
            foreach (var group in _groupCells)
            {
                if (group.StartPauseTimer.IsFull)
                {
                    if (group.SubgroupsPauseTimer.IsFull)
                    {
                        if (group.EnemiesPauseTimer.IsFull)
                        {
                            OnSpawnEnemy?.Invoke(group.EnemyGroup.enemySubgroup[group.EnemyIndex], group.EnemyGroup);
                            group.IncreaseEnemyIndex();
                        }
                        else
                            group.EnemiesPauseTimer.ChangeCurrentValue(deltaTime);
                    }
                    else
                        group.SubgroupsPauseTimer.ChangeCurrentValue(deltaTime);
                }
                else
                    group.StartPauseTimer.ChangeCurrentValue(deltaTime);
            }

            // _currentGroupsCount++;
            //
            // EnemyGroupConfig enemyGroup = _enemyWaves[_waveIndex].enemyWave[groupIndex];
            //
            // if(enemyGroup.timePause > 0) 
            //     yield return new WaitForSeconds(enemyGroup.timePause);
            //
            // float timePauseBetweenEnemies = enemyGroup.distanceBetweenEnemies / enemyGroup.moveSpeed;
            // float timePauseBetweenSubgroup = enemyGroup.distanceBetweenSubgroups / enemyGroup.moveSpeed;
            // for (int i = 0; i < enemyGroup.subgroupsCount; i++)
            // {
            //     for (int j = 0; j < enemyGroup.enemySubgroup.Count; j++)
            //     {
            //         if(_spaceShipsPool.ExtractElement(enemyGroup.enemySubgroup[j], out EnemySpaceshipBase enemySpaceShip))
            //         {
            //             enemySpaceShip.SetWaveData(enemyGroup.moveSpeed,enemyGroup.path,enemyGroup.endOfPathInstruction,enemyGroup.pathWayMoveType,enemyGroup.rotationType,enemyGroup.accelerated,enemyGroup.acceleration);
            //         }
            //         yield return new WaitForSeconds(timePauseBetweenEnemies);
            //     }
            //     yield return new WaitForSeconds(timePauseBetweenSubgroup);
            // }
            //
            // _currentGroupsCount--;
        }
        
        private struct GroupCell
        {
            public readonly EnemyGroupConfig EnemyGroup; 
            
            public readonly SomeStorageFloat StartPauseTimer;
            public readonly SomeStorageFloat SubgroupsPauseTimer;
            public readonly SomeStorageFloat EnemiesPauseTimer;

            public int EnemyIndex;
            public int SubgroupIndex;

            public GroupCell(EnemyGroupConfig enemyGroup)
            {
                EnemyGroup = enemyGroup;
                
                StartPauseTimer = new SomeStorageFloat(EnemyGroup.StartTimePause);
                SubgroupsPauseTimer = new SomeStorageFloat(EnemyGroup.distanceBetweenSubgroups / EnemyGroup.moveSpeed);
                EnemiesPauseTimer = new SomeStorageFloat(EnemyGroup.distanceBetweenEnemies / EnemyGroup.moveSpeed);

                EnemyIndex = 0;
                SubgroupIndex = 0;
            }

            public void IncreaseEnemyIndex()
            {
                EnemyIndex++;

                if (EnemyIndex >= EnemyGroup.enemySubgroup.Count)
                {
                    SubgroupIndex++;
                    EnemyIndex = 0;
                }
            }
        }
    }
}