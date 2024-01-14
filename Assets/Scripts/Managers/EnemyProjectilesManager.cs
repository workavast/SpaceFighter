using System;
using System.Collections.Generic;
using System.Linq;
using Factories;
using GameCycle;
using PoolSystem;
using Projectiles.Enemy;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class EnemyProjectilesManager : ManagerBase
    {
        protected override GameStatesType GameStatesType => GameStatesType.Gameplay;
    
        private readonly Dictionary<EnemyProjectileType, GameObject> _projectilesParents = new();

        [Inject] private EnemyProjectilesFactory _enemyProjectilesFactory;

        private Pool<EnemyProjectileBase, EnemyProjectileType> _pool;
    
        protected override void OnAwake()
        {
            _pool = new Pool<EnemyProjectileBase, EnemyProjectileType>(EnemyProjectileInstantiate);
        
            foreach (var enemyShipId in Enum.GetValues(typeof(EnemyProjectileType)).Cast<EnemyProjectileType>())
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

        private EnemyProjectileBase EnemyProjectileInstantiate(EnemyProjectileType id)
            => _enemyProjectilesFactory.Create(id, _projectilesParents[id].transform).GetComponent<EnemyProjectileBase>();

        public bool TrySpawnProjectile(EnemyProjectileType id, Transform newTransform, out EnemyProjectileBase projectileBase)
        {
            if (_pool.ExtractElement(id, out EnemyProjectileBase newProjectile))
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
    
        private void ReturnProjectileInPool(EnemyProjectileBase projectile)
        {
            projectile.OnLifeTimeEnd -= ReturnProjectileInPool;
            _pool.ReturnElement(projectile);
        }
    }
}
