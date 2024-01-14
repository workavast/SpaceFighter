using System;
using Configs;
using GameCycle;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class PlayerSpaceshipManager : ManagerBase
    {
        protected override GameStatesType GameStatesType => GameStatesType.Gameplay;
        
        [SerializeField] private PlayerSpaceship playerSpaceship;
        
        [Inject] private PlayerWeaponConfig _playerWeaponConfig;
        [Inject] private PlayerSpaceshipLevelsConfig _playerSpaceshipLevelsConfig;
        [Inject] private DiContainer _diContainer;
        
        private PlayerWeaponBase _weapon;

        public event Action OnPlayerDie;
        public bool PlayerIsDead { get; private set; }
        
        protected override void OnAwake()
        {
            playerSpaceship.Initialization(_playerSpaceshipLevelsConfig);
            playerSpaceship.OnDead += OnPlayerDead;
            
            SpawnWeapon();
        }
        
        public override void GameCycleUpdate()
        {
            playerSpaceship.HandleUpdate(Time.deltaTime);
            _weapon.HandleUpdate();
        }
        
        private void SpawnWeapon()
        {
            if (_playerWeaponConfig.WeaponsPrefabsData.TryGetValue(PlayerGlobalData.EquippedPlayerWeapon, out GameObject prefab))
            {
                GameObject weapon = _diContainer.InstantiatePrefab(prefab, playerSpaceship.WeaponPosition);
                _weapon = weapon.GetComponent<PlayerWeaponBase>();
                _weapon.Initialization();
            }
            else
                throw new Exception("Dictionary don't contain this WeaponsEnum");
        }
        
        private void OnPlayerDead()
        {
            _weapon.StopShoot();
            OnPlayerDie?.Invoke();
        }
    }
}