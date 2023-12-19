using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PoolSystem;
using UnityEngine;
using Zenject;

public class MoneyStarsManager : MonoBehaviour
{
    private static MoneyStarsManager _instance;
    
    private Pool<MoneyStar> _projectilesPool;
    
    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        
        _projectilesPool = new Pool<MoneyStar>(EnemySpaceShipInstantiate);
    }
    
    private void Update()
    {
        IReadOnlyList<IHandleUpdate> list = _projectilesPool.BusyElements;
        for (int i = 0; i < list.Count; i++)
            list[i].HandleUpdate();
    }
    
    private MoneyStar EnemySpaceShipInstantiate()
    {
        return MoneyStarsFactory.Create(transform).GetComponentInChildren<MoneyStar>();
    }

    public static void Spawn(Transform newTransform)
    {
        if (_instance._projectilesPool.ExtractElement(out MoneyStar newProjectile))
        {
            newProjectile.transform.position = newTransform.position;
        }
        else
        {
            Debug.LogWarning("There was no extraction");
        }
    } 
}
