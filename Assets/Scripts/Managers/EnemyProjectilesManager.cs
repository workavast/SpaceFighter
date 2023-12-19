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
    
    private Pool<EnemyProjectileBase, EnemyProjectilesEnum> _projectilesPool;

    private Dictionary<EnemyProjectilesEnum, GameObject> _projectilesParents = new Dictionary<EnemyProjectilesEnum, GameObject>();

    protected override void OnAwake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        
        _projectilesPool = new Pool<EnemyProjectileBase, EnemyProjectilesEnum>(EnemySpaceShipInstantiate);
        
        foreach (var enemyShipId in Enum.GetValues(typeof(EnemyProjectilesEnum)).Cast<EnemyProjectilesEnum>())
        {
            GameObject parent = new GameObject(enemyShipId.ToString()) { transform = { parent = transform } };
            _projectilesParents.Add(enemyShipId, parent);
        }
    }

    public override void GameCycleUpdate()
    {
        IReadOnlyList<IReadOnlyList<IHandleUpdate>> list = _projectilesPool.BusyElementsValues;
        for (int i = 0; i < list.Count(); i++)
            for (int j = 0; j < list[i].Count; j++)
                list[i][j].HandleUpdate();
    }

    private EnemyProjectileBase EnemySpaceShipInstantiate(EnemyProjectilesEnum id)
    {
        return EnemyProjectilesFactory.Create(id, _projectilesParents[id].transform).GetComponentInChildren<EnemyProjectileBase>();
    }

    public static bool SpawnProjectile(EnemyProjectilesEnum id, Transform newTransform, out EnemyProjectileBase projectileBase)
    {
        if (_instance._projectilesPool.ExtractElement(id, out EnemyProjectileBase newProjectile))
        {
            projectileBase = newProjectile;
            newProjectile.transform.position = newTransform.position;
            newProjectile.transform.rotation = newTransform.rotation;
            return true;
        }
        else
        {
            projectileBase = null;
            Debug.LogWarning("There was no extraction");
            return false;
        }
    }
}
