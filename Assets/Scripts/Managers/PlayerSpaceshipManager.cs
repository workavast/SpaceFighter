﻿using System;
using Configs;
using GameCycle;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Managers
{
    public class PlayerSpaceshipManager : ManagerBase
    {
        protected override GameStatesType GameStatesType => GameStatesType.Gameplay;
        
        [field: SerializeField] public PlayerSpaceship PlayerSpaceship { get; private set; }
        
        [Inject] private PlayerWeaponConfig _playerWeaponConfig;
        [Inject] private PlayerSpaceshipLevelsConfig _playerSpaceshipLevelsConfig;
        [Inject] private DiContainer _diContainer;
        
        private PlayerWeaponBase _weapon;

        public event Action OnPlayerDie;
        public bool PlayerIsDead { get; private set; }
        
        protected override void OnAwake()
        {
            PlayerSpaceship.Initialization(_playerSpaceshipLevelsConfig);
            PlayerSpaceship.OnDead += OnPlayerDead;
            
            SpawnWeapon();
        }
        
        public override void GameCycleUpdate()
        {
            PlayerSpaceship.HandleUpdate(Time.deltaTime);
            _weapon.HandleUpdate();
        }
        
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
    }
}