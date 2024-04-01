using System;
using System.Collections.Generic;
using System.Linq;
using Configs.Missions;
using EventBusEvents;
using EventBusExtension;
using Factories;
using GameCycle;
using UnityEngine;
using PoolSystem;
using SpaceShips.Enemies;
using Zenject;

namespace Managers
{
    public class EnemySpaceshipsManager : GameCycleManager, IGameCycleEnter, IGameCycleExit, IEventReceiver<SpawnEnemy>
    {
        protected override GameCycleState GameCycleState => GameCycleState.Gameplay;
    
        private readonly Dictionary<EnemySpaceshipType, GameObject> _spaceshipsParents = new();

        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();

        private Pool<EnemySpaceshipBase, EnemySpaceshipType> _pool;

        [Inject] private EnemySpaceshipsFactory _enemySpaceshipsFactory;
        [Inject] private EventBus _eventBus;
        
        public int ActiveEnemiesCount => _pool.BusyElementsValues.Sum(e => e.Count);

        public event Action OnAllEnemiesGone;

        protected override void OnAwake()
        {
            base.OnAwake();
            
            GameCycleController.AddListener(GameCycleState, this as IGameCycleEnter);
            GameCycleController.AddListener(GameCycleState, this as IGameCycleExit);
            
            _eventBus.Subscribe(this);
            _pool = new Pool<EnemySpaceshipBase, EnemySpaceshipType>(EnemySpaceShipInstantiate);
        
            foreach (var enemyShipId in Enum.GetValues(typeof(EnemySpaceshipType)).Cast<EnemySpaceshipType>())
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
    
        public void GameCycleEnter()
        {
            foreach (var enemies in _pool.BusyElementsValues)
                foreach (var enemy in enemies)
                    enemy.ChangeAnimatorState(true);
        }

        public void GameCycleExit()
        {
            foreach (var enemies in _pool.BusyElementsValues)
                foreach (var enemy in enemies)
                    enemy.ChangeAnimatorState(false);
        }
        
        public void OnEvent(SpawnEnemy @event) => SpawnEnemy(@event.EnemySpaceshipType, @event.EnemyGroupConfig);

        private EnemySpaceshipBase EnemySpaceShipInstantiate(EnemySpaceshipType id)
        {
            var enemySpaceship = _enemySpaceshipsFactory.Create(id, _spaceshipsParents[id].transform)
                .GetComponent<EnemySpaceshipBase>();
            enemySpaceship.OnGone += ReturnEnemy;
            return enemySpaceship;
        }

        private void SpawnEnemy(EnemySpaceshipType enemySpaceshipsType, EnemyGroupConfig config)
        {
            if(_pool.ExtractElement(enemySpaceshipsType, out EnemySpaceshipBase enemySpaceShip))
                enemySpaceShip.SetWaveData(config.MoveSpeed, config.Path, config.PathWayMoveType, config.RotationType);
        }

        private void ReturnEnemy(EnemySpaceshipBase enemy)
        {
            _pool.ReturnElement(enemy);
            
            if(ActiveEnemiesCount <= 0)
                OnAllEnemiesGone?.Invoke();
        }

        protected override void OnDestroyVirtual()
        {
            base.OnDestroyVirtual();
            
            _eventBus.UnSubscribe(this);
            
            GameCycleController.RemoveListener(GameCycleState, this as IGameCycleEnter);
            GameCycleController.RemoveListener(GameCycleState, this as IGameCycleExit);
        }
    }
}