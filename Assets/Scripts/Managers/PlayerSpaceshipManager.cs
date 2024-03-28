using System;
using Configs;
using Control;
using GameCycle;
using Initializers;
using PlayerWeapon;
using Saves;
using SpaceShips;
using UnityEngine;
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
        [Inject] private MobileInputDetector _mobileInputDetector;
       
        private IInput _input;
        private PlayerWeaponBase _weapon;

        public event Action OnPlayerDie;
        public bool PlayerIsDead { get; private set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            switch (PlayerGlobalData.Instance.PlatformType)
            {
                case PlatformType.Mobile:
                    _input = new MobileInput(_mobileInputDetector, PlayerSpaceship.transform);
                    break;
                case PlatformType.Desktop:
                    _input = new DesktopInput(Camera.main);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            GameCycleController.AddListener(GameCycleState, this as IGameCycleEnter);
            GameCycleController.AddListener(GameCycleState, this as IGameCycleExit);
            
            PlayerSpaceship.Initialization(_playerSpaceshipLevelsConfig);
            PlayerSpaceship.OnDead += OnPlayerDead;
            
            SpawnWeapon();
        }
        
        public override void GameCycleUpdate()
        {
            PlayerSpaceship.Move(_input.Position());
            _weapon.HandleUpdate();
        }
        
        public void GameCycleEnter() => PlayerSpaceship.ChangeAnimatorState(true);

        public void GameCycleExit() => PlayerSpaceship.ChangeAnimatorState(false);
        
        private void SpawnWeapon()
        {
            if (_playerWeaponConfig.WeaponsPrefabsData.TryGetValue(PlayerGlobalData.Instance.WeaponsSettings.EquippedPlayerWeapon, out GameObject prefab))
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