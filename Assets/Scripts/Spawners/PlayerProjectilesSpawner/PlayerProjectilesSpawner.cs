using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PoolSystem;

public class PlayerProjectilesSpawner : MonoBehaviour
{
    private static PlayerProjectilesSpawner _instance;
    
    private Pool<PlayerProjectileBase, PlayerProjectilesEnum> _projectilesPool;

    private Dictionary<PlayerProjectilesEnum, GameObject> _projectilesParents = new Dictionary<PlayerProjectilesEnum, GameObject>();

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        
        _projectilesPool = new Pool<PlayerProjectileBase, PlayerProjectilesEnum>(EnemySpaceShipInstantiate);
        
        foreach (var enemyShipId in Enum.GetValues(typeof(PlayerProjectilesEnum)).Cast<PlayerProjectilesEnum>())
        {
            GameObject parent = new GameObject(enemyShipId.ToString()) { transform = { parent = transform } };
            _projectilesParents.Add(enemyShipId, parent);
        }
    }
    
    private void Update()
    {
        IReadOnlyList<IReadOnlyList<IHandleUpdate>> list = _projectilesPool.BusyElementsValues;
        for (int i = 0; i < list.Count(); i++)
        for (int j = 0; j < list[i].Count; j++)
            list[i][j].HandleUpdate();
    }
    
    private PlayerProjectileBase EnemySpaceShipInstantiate(PlayerProjectilesEnum id)
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
