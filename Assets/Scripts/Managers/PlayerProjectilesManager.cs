using System;
using System.Collections.Generic;
using System.Linq;
using GameCycle;
using Managers;
using UnityEngine;
using PoolSystem;

public class PlayerProjectilesManager : ManagerBase
{
    protected override GameStatesType GameStatesType => GameStatesType.Gameplay;

    private static PlayerProjectilesManager _instance;
    
    private Pool<PlayerProjectileBase, PlayerProjectilesEnum> _projectilesPool;

    private Dictionary<PlayerProjectilesEnum, GameObject> _projectilesParents = new Dictionary<PlayerProjectilesEnum, GameObject>();

    protected override void OnAwake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        
        _projectilesPool = new Pool<PlayerProjectileBase, PlayerProjectilesEnum>(PlayerProjectileInstantiate);
        
        foreach (var enemyShipId in Enum.GetValues(typeof(PlayerProjectilesEnum)).Cast<PlayerProjectilesEnum>())
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
    
    private PlayerProjectileBase PlayerProjectileInstantiate(PlayerProjectilesEnum id)
    {
        return PlayerProjectilesFactory.Create(id, _projectilesParents[id].transform).GetComponentInChildren<PlayerProjectileBase>();
    }

    public static bool SpawnProjectile(PlayerProjectilesEnum id, Transform newTransform, out PlayerProjectileBase projectileBase)
    {
        if (_instance._projectilesPool.ExtractElement(id, out PlayerProjectileBase newProjectile))
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
