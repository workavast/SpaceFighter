using System;
using System.Collections.Generic;
using EventBusExtension;
using Factories;
using GameCycle;
using UnityEngine;
using SpaceShips.Enemies;
using Zenject;

namespace Managers
{
    public class EnemySpaceshipsManager : GameCycleManager, IGameCycleEnter, IGameCycleExit
    {
        [Inject] private EnemySpaceshipsFactory _enemySpaceshipsFactory;
        
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();
        public int ActiveEnemiesCount => _enemySpaceshipsRepository.Enemies.Count;
        
        protected override GameCycleState GameCycleState => GameCycleState.Gameplay;
        
        private EnemySpaceshipsRepository _enemySpaceshipsRepository;
        private EnemySpaceshipsUpdater _enemySpaceshipsUpdater;
        
        public event Action OnAllEnemiesGone;

        protected override void OnAwake()
        {
            base.OnAwake();
            
            GameCycleController.AddListener(GameCycleState, this as IGameCycleEnter);
            GameCycleController.AddListener(GameCycleState, this as IGameCycleExit);
            
            _enemySpaceshipsRepository = new EnemySpaceshipsRepository(_enemySpaceshipsFactory);
            _enemySpaceshipsUpdater = new EnemySpaceshipsUpdater(_enemySpaceshipsRepository);

            _enemySpaceshipsRepository.OnRemove += OnRemoveEnemy;
        }
    
        public void GameCycleEnter()
        {
            IReadOnlyList<EnemySpaceshipBase> list = _enemySpaceshipsRepository.Enemies;
            for (int j = 0; j < list.Count; j++)
                list[j].ChangeAnimatorState(true);
        }

        public void GameCycleExit()
        {
            IReadOnlyList<EnemySpaceshipBase> list = _enemySpaceshipsRepository.Enemies;
            for (int j = 0; j < list.Count; j++)
                list[j].ChangeAnimatorState(false);
        }
        
        public override void GameCycleUpdate()
            => _enemySpaceshipsUpdater.HandleUpdate(Time.deltaTime);
        
        private void OnRemoveEnemy(EnemySpaceshipBase enemy)
        {
            if(ActiveEnemiesCount <= 0)
                OnAllEnemiesGone?.Invoke();
        }

        protected override void OnDestroyVirtual()
        {
            base.OnDestroyVirtual();
            
            GameCycleController.RemoveListener(GameCycleState, this as IGameCycleEnter);
            GameCycleController.RemoveListener(GameCycleState, this as IGameCycleExit);
        }
    }
}