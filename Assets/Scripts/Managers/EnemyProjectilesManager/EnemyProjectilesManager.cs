using Factories;
using GameCycle;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class EnemyProjectilesManager : GameCycleManager, IGameCycleEnter, IGameCycleExit
    {
        protected override GameCycleState GameCycleState => GameCycleState.Gameplay;
        
        [Inject] private EnemyProjectilesFactory _enemyProjectilesFactory;

        private EnemyProjectilesRepository _enemyProjectilesRepository;
        private EnemyProjectilesUpdater _enemyProjectilesUpdater;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _enemyProjectilesRepository = new EnemyProjectilesRepository(_enemyProjectilesFactory);
            _enemyProjectilesUpdater = new EnemyProjectilesUpdater(_enemyProjectilesRepository);
            
            GameCycleController.AddListener(GameCycleState, this as IGameCycleEnter);
            GameCycleController.AddListener(GameCycleState, this as IGameCycleExit);
        }
        
        public void GameCycleEnter()
            => _enemyProjectilesUpdater.GameCycleEnter();
        
        public void GameCycleExit()
            => _enemyProjectilesUpdater.GameCycleExit();
        
        public override void GameCycleUpdate()
            => _enemyProjectilesUpdater.HandleUpdate(Time.deltaTime);

        protected override void OnDestroyVirtual()
        {
            base.OnDestroyVirtual();
            
            GameCycleController.RemoveListener(GameCycleState, this as IGameCycleEnter);
            GameCycleController.RemoveListener(GameCycleState, this as IGameCycleExit);
        }
    }
}
