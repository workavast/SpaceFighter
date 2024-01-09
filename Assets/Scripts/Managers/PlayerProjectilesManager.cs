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
    
    private Pool<PlayerProjectileBase, PlayerProjectilesEnum> _pool;

    private readonly Dictionary<PlayerProjectilesEnum, GameObject> _projectilesParents = new();

    protected override void OnAwake()
    {
        _pool = new Pool<PlayerProjectileBase, PlayerProjectilesEnum>(PlayerProjectileInstantiate);
        
        foreach (var enemyShipId in Enum.GetValues(typeof(PlayerProjectilesEnum)).Cast<PlayerProjectilesEnum>())
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
    
    private PlayerProjectileBase PlayerProjectileInstantiate(PlayerProjectilesEnum id)
        => PlayerProjectilesFactory.Create(id, _projectilesParents[id].transform).GetComponent<PlayerProjectileBase>();

    public bool TrySpawnProjectile(PlayerProjectilesEnum id, Transform newTransform, out PlayerProjectileBase projectileBase)
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

    private void ReturnProjectileInPool(PlayerProjectileBase projectile)
    {
        projectile.OnLifeTimeEnd -= ReturnProjectileInPool;
        _pool.ReturnElement(projectile);
    }
}
