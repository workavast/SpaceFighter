using System;
using System.Collections.Generic;
using System.Linq;
using Configs.Missions;
using Factories;
using GameCycle;
using UnityEngine;
using MissionsDataConfigsSystem;
using PoolSystem;
using Zenject;

namespace Managers
{
    public class EnemySpaceshipsManager : ManagerBase
    {
        protected override GameStatesType GameStatesType => GameStatesType.Gameplay;
    
        private readonly Dictionary<EnemySpaceshipsEnum, GameObject> _spaceshipsParents = new();

        private Pool<EnemySpaceshipBase, EnemySpaceshipsEnum> _pool;

        [Inject] private EnemySpaceshipsFactory _enemySpaceshipsFactory;
        
        public int ActiveEnemiesCount => _pool.BusyElementsValues.Sum(e => e.Count);

        public event Action OnAllEnemiesGone;

        protected override void OnAwake()
        {
            _pool = new Pool<EnemySpaceshipBase, EnemySpaceshipsEnum>(EnemySpaceShipInstantiate);
        
            foreach (var enemyShipId in Enum.GetValues(typeof(EnemySpaceshipsEnum)).Cast<EnemySpaceshipsEnum>())
            {
                GameObject parent = new GameObject(enemyShipId.ToString()) { transform = { parent = transform } };
                _spaceshipsParents.Add(enemyShipId, parent);
            }
        }
    
        public override void GameCycleUpdate()
        {
            IReadOnlyList<IReadOnlyList<IHandleUpdate>> list = _pool.BusyElementsValues;
            for (int i = 0; i < list.Count(); i++)
            for (int j = 0; j < list[i].Count; j++)
                list[i][j].HandleUpdate(Time.deltaTime);
        }
    
        private EnemySpaceshipBase EnemySpaceShipInstantiate(EnemySpaceshipsEnum id)
        {
            var enemySpaceship = _enemySpaceshipsFactory.Create(id, _spaceshipsParents[id].transform)
                .GetComponent<EnemySpaceshipBase>();
            enemySpaceship.OnGone += ReturnEnemy;
            return enemySpaceship;
        }

        public void SpawnEnemy(EnemySpaceshipsEnum enemySpaceshipsType, EnemyGroupConfig config)
        {
            if(_pool.ExtractElement(enemySpaceshipsType, out EnemySpaceshipBase enemySpaceShip))
            {
                enemySpaceShip.SetWaveData(config.moveSpeed, config.path, config.endOfPathInstruction,
                    config.pathWayMoveType, config.rotationType, config.accelerated,
                    config.acceleration);
            }
        }
        
        private void ReturnDeadEnemy(EnemySpaceshipBase enemy) => ReturnEnemy(enemy);

        private void ReturnEscapedEnemy(EnemySpaceshipBase enemy) => ReturnEnemy(enemy);

        private void ReturnEnemy(EnemySpaceshipBase enemy)
        {
            _pool.ReturnElement(enemy);
            
            if(ActiveEnemiesCount <= 0)
                OnAllEnemiesGone?.Invoke();
        }
    }
}