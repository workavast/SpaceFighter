using System.Collections.Generic;
using Factories;
using GameCycle;
using PoolSystem;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class MoneyStarsManager : ManagerBase
    {
        protected override GameStatesType GameStatesType => GameStatesType.Gameplay;
    
        private Pool<MoneyStar> _pool;

        [Inject] private MoneyStarsFactory _moneyStarsFactory;
    
        protected override void OnAwake()
        {
            _pool = new Pool<MoneyStar>(MoneyStarInstantiate);
        }
    
        public override void GameCycleUpdate()
        {
            IReadOnlyList<IHandleUpdate> list = _pool.BusyElements;
            for (int i = 0; i < list.Count; i++)
                list[i].HandleUpdate(Time.deltaTime);
        }
    
        private MoneyStar MoneyStarInstantiate()
        {
            var moneyStar = _moneyStarsFactory.Create(transform).GetComponentInChildren<MoneyStar>();
            moneyStar.OnStarTaking += OnMoneyStarTaking;
            moneyStar.OnLoseStar += ReturnStarInPool;
        
            return moneyStar;
        }

        private void OnMoneyStarTaking(MoneyStar moneyStar)
        {
            LevelMoneyStarsCounter.ChangeValue(1);
            ReturnStarInPool(moneyStar);
        }
    
        private void ReturnStarInPool(MoneyStar moneyStar) => _pool.ReturnElement(moneyStar);
    
        public void Spawn(Transform newTransform)
        {
            if (_pool.ExtractElement(out MoneyStar newProjectile))
                newProjectile.transform.position = newTransform.position;
            else
                Debug.LogWarning("There was no extraction");
        }
    }
}
