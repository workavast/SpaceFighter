using System.Collections.Generic;
using GameCycle;
using Managers;
using PoolSystem;
using UnityEngine;

public class MoneyStarsManager : ManagerBase
{
    protected override GameStatesType GameStatesType => GameStatesType.Gameplay;

    private static MoneyStarsManager _instance;
    
    private Pool<MoneyStar> _projectilesPool;
    
    protected override void OnAwake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        
        _projectilesPool = new Pool<MoneyStar>(EnemySpaceShipInstantiate);
    }
    
    public override void GameCycleUpdate()
    {
        IReadOnlyList<IHandleUpdate> list = _projectilesPool.BusyElements;
        for (int i = 0; i < list.Count; i++)
            list[i].HandleUpdate(Time.deltaTime);
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
