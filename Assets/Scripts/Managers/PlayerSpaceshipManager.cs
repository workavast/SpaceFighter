using System;
using GameCycle;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class PlayerSpaceshipManager : ManagerBase
    {
        protected override GameStatesType GameStatesType => GameStatesType.Gameplay;
        
        [SerializeField] private PlayerSpaceship _playerSpaceship;
        
        [Inject] private IGameCycleManager _gameCycleManager;
        [Inject] private PlayerWeaponConfig _playerWeaponConfig;
        [Inject] private PlayerSpaceshipLevelsConfig _playerSpaceshipLevelsConfig;
        [Inject] private DiContainer _diContainer;
        
        private PlayerWeaponBase _weapon;

        protected override void OnAwake()
        {
            _playerSpaceship.Initialization(_playerSpaceshipLevelsConfig);
            _playerSpaceship.OnDead += PlayerIsDead;
            
            SpawnWeapon();
        }
        
        public override void GameCycleUpdate()
        {
            _playerSpaceship.HandleUpdate();
            _weapon.HandleUpdate();
        }
        
        private void SpawnWeapon()
        {
            if (_playerWeaponConfig.WeaponsPrefabsData.TryGetValue(PlayerGlobalData.EquippedPlayerWeapon, out GameObject prefab))
            {
                GameObject weapon = _diContainer.InstantiatePrefab(prefab, _playerSpaceship.WeaponPosition);
                _weapon = weapon.GetComponent<PlayerWeaponBase>();
                _weapon.Initialization();
            }
            else
                throw new Exception("Dictionary don't contain this WeaponsEnum");
        }
        
        private void PlayerIsDead()
        {
            _weapon.StopShoot();
            _gameCycleManager.SwitchState(GameStatesType.Pause);
        }
    }
}