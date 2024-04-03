using Factories;
using GameCycle;
using Projectiles.Player;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class PlayerProjectilesManager : GameCycleManager, IGameCycleEnter, IGameCycleExit
    {
        protected override GameCycleState GameCycleState => GameCycleState.Gameplay;
        
        [Inject] private PlayerProjectilesFactory _playerProjectilesFactory;

        private PlayerProjectilesRepository _playerProjectilesRepository;
        private PlayerProjectilesUpdater _playerProjectilesUpdater;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _playerProjectilesRepository = new PlayerProjectilesRepository(_playerProjectilesFactory);
            _playerProjectilesUpdater = new PlayerProjectilesUpdater(_playerProjectilesRepository);

            _playerProjectilesRepository.OnAdd += InitProjectile;
            
            GameCycleController.AddListener(GameCycleState, this as IGameCycleEnter);
            GameCycleController.AddListener(GameCycleState, this as IGameCycleExit);
        }
    
        public override void GameCycleUpdate()
            => _playerProjectilesUpdater.HandleUpdate(Time.deltaTime);
    
        public void GameCycleEnter()
            => _playerProjectilesUpdater.GameCycleEnter();

        public void GameCycleExit()
            => _playerProjectilesUpdater.GameCycleExit();

        private void InitProjectile(PlayerProjectileBase projectile)
            => projectile.Init(GameCycleController.CurrentState == GameCycleState);
        
        protected override void OnDestroyVirtual()
        {
            base.OnDestroyVirtual();
            
            GameCycleController.RemoveListener(GameCycleState, this as IGameCycleEnter);
            GameCycleController.RemoveListener(GameCycleState, this as IGameCycleExit);
        }
    }
}
