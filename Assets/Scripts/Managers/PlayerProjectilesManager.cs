using System;
using System.Collections.Generic;
using System.Linq;
using Factories;
using GameCycle;
using PoolSystem;
using Projectiles.Player;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class PlayerProjectilesManager : GameCycleManager, IGameCycleEnter, IGameCycleExit
    {
        protected override GameCycleState GameCycleState => GameCycleState.Gameplay;
    
        private Pool<PlayerProjectileBase, PlayerProjectileType> _pool;

        private readonly Dictionary<PlayerProjectileType, GameObject> _projectilesParents = new();

        [Inject] private PlayerProjectilesFactory _playerProjectilesFactory;
    
        protected override void OnAwake()
        {
            base.OnAwake();

            GameCycleController.AddListener(GameCycleState, this as IGameCycleEnter);
            GameCycleController.AddListener(GameCycleState, this as IGameCycleExit);
            
            _pool = new Pool<PlayerProjectileBase, PlayerProjectileType>(PlayerProjectileInstantiate);
        
            foreach (var enemyShipId in Enum.GetValues(typeof(PlayerProjectileType)).Cast<PlayerProjectileType>())
            {
                GameObject parent = new GameObject(enemyShipId.ToString()) { transform = { parent = transform } };
                _projectilesParents.Add(enemyShipId, parent);
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
            foreach (var projectiles in _pool.BusyElementsValues)
            foreach (var projectile in projectiles)
                projectile.ChangeAnimatorState(true);
        }

        public void GameCycleExit()
        {
            foreach (var projectiles in _pool.BusyElementsValues)
            foreach (var projectile in projectiles)
                projectile.ChangeAnimatorState(false);
        }

        public bool TrySpawnProjectile(PlayerProjectileType id, Transform newTransform, out PlayerProjectileBase projectileBase)
        {
            if (_pool.ExtractElement(id, out PlayerProjectileBase newProjectile))
            {
                projectileBase = newProjectile;
                newProjectile.transform.position = newTransform.position;
                newProjectile.transform.rotation = newTransform.rotation;
                newProjectile.OnLifeTimeEnd += ReturnProjectileInPool;
                return true;
            }
            else
            {
                projectileBase = null;
                Debug.LogWarning("There was no extraction");
                return false;
            }
        }

        private PlayerProjectileBase PlayerProjectileInstantiate(PlayerProjectileType id)
            => _playerProjectilesFactory.Create(id, _projectilesParents[id].transform).GetComponent<PlayerProjectileBase>();
        
        private void ReturnProjectileInPool(PlayerProjectileBase projectile)
        {
            projectile.OnLifeTimeEnd -= ReturnProjectileInPool;
            _pool.ReturnElement(projectile);
        }

        protected override void OnDestroyVirtual()
        {
            base.OnDestroyVirtual();
            
            GameCycleController.RemoveListener(GameCycleState, this as IGameCycleEnter);
            GameCycleController.RemoveListener(GameCycleState, this as IGameCycleExit);
        }
    }
}
