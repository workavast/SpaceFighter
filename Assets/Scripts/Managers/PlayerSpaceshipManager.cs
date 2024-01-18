using System;
using CastExtension;
using Configs;
using GameCycle;
using Projectiles.Player;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Managers
{
    public class PlayerSpaceshipManager : GameCycleManager, IGameCycleEnter, IGameCycleExit
    {
        protected override GameCycleState GameCycleState => GameCycleState.Gameplay;
        
        [field: SerializeField] public PlayerSpaceship PlayerSpaceship { get; private set; }
        
        [Inject] private PlayerWeaponConfig _playerWeaponConfig;
        [Inject] private PlayerSpaceshipLevelsConfig _playerSpaceshipLevelsConfig;
        [Inject] private DiContainer _diContainer;
        
        private PlayerWeaponBase _weapon;

        public event Action OnPlayerDie;
        public bool PlayerIsDead { get; private set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            GameCycleController.AddListener(GameCycleState, this as IGameCycleEnter);
            GameCycleController.AddListener(GameCycleState, this as IGameCycleExit);
            
            PlayerSpaceship.Initialization(_playerSpaceshipLevelsConfig);
            PlayerSpaceship.OnDead += OnPlayerDead;
            
            SpawnWeapon();
        }
        
        public override void GameCycleUpdate()
        {
            PlayerSpaceship.HandleUpdate(Time.deltaTime);
            _weapon.HandleUpdate();
        }
        
        public void GameCycleEnter() => PlayerSpaceship.ChangeAnimatorState(true);

        public void GameCycleExit() => PlayerSpaceship.ChangeAnimatorState(false);
        
        private void SpawnWeapon()
        {
            if (_playerWeaponConfig.WeaponsPrefabsData.TryGetValue(PlayerGlobalData.EquippedPlayerWeapon, out GameObject prefab))
            {
                GameObject weapon = _diContainer.InstantiatePrefab(prefab, PlayerSpaceship.WeaponPosition);
                _weapon = weapon.GetComponent<PlayerWeaponBase>();
                _weapon.Initialization();
            }
            else
                throw new Exception("Dictionary don't contain this WeaponsEnum");
        }
        
        private void OnPlayerDead()
        {
            _weapon.StopShoot();
            PlayerIsDead = true;
            OnPlayerDie?.Invoke();
        }

        protected override void OnDestroyVirtual()
        {
            base.OnDestroyVirtual();
            
            GameCycleController.RemoveListener(GameCycleState, this as IGameCycleEnter);
            GameCycleController.RemoveListener(GameCycleState, this as IGameCycleExit);
        }
    }
}