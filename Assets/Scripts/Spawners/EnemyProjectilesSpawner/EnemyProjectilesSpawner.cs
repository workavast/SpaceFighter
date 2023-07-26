using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyProjectilesSpawner : MonoBehaviour
{
    private static EnemyProjectilesSpawner _instance;
    
    private Pool<EnemyProjectileBase, EnemyProjectilesEnum> _projectilesPool;

    private Dictionary<EnemyProjectilesEnum, GameObject> _projectilesParents = new Dictionary<EnemyProjectilesEnum, GameObject>();

    private void Awake()
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

    private void Update()
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
