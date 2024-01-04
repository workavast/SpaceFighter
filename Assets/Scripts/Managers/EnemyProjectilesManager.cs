using System;
using System.Collections.Generic;
using System.Linq;
using GameCycle;
using Managers;
using UnityEngine;
using PoolSystem;

public class EnemyProjectilesManager : ManagerBase
{
    protected override GameStatesType GameStatesType => GameStatesType.Gameplay;

    private static EnemyProjectilesManager _instance;
    
    private Pool<EnemyProjectileBase, EnemyProjectilesEnum> _pool;

    private readonly Dictionary<EnemyProjectilesEnum, GameObject> _projectilesParents = new();

    protected override void OnAwake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        
        _pool = new Pool<EnemyProjectileBase, EnemyProjectilesEnum>(EnemyProjectileInstantiate);
        
        foreach (var enemyShipId in Enum.GetValues(typeof(EnemyProjectilesEnum)).Cast<EnemyProjectilesEnum>())
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

    private EnemyProjectileBase EnemyProjectileInstantiate(EnemyProjectilesEnum id)
        => EnemyProjectilesFactory.Create(id, _projectilesParents[id].transform).GetComponent<EnemyProjectileBase>();

    public static bool TrySpawnProjectile(EnemyProjectilesEnum id, Transform newTransform, out EnemyProjectileBase projectileBase)
    {
        if (_instance._pool.ExtractElement(id, out EnemyProjectileBase newProjectile))
        {
            projectileBase = newProjectile;
            newProjectile.transform.position = newTransform.position;
            newProjectile.transform.rotation = newTransform.rotation;
            newProjectile.OnLifeTimeEnd += _instance.ReturnProjectileInPool;
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
