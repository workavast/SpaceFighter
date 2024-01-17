using System.Collections.Generic;
using EventBus;
using EventBus.Events;
using EventBusExtension;
using Factories;
using GameCycle;
using PoolSystem;
using SomeStorages;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class MoneyStarsManager : GameCycleManager, IEventReceiver<EnemyStartDie>
    {
        protected override GameStatesType GameStatesType => GameStatesType.Gameplay;

        public ReceiverIdentifier ReceiverIdentifier { get; } = new();
   
        [Inject] private MoneyStarsFactory _moneyStarsFactory;
        [Inject] private MissionEventBus _missionEventBus;
        
        private Pool<MoneyStar> _pool;

        private SomeStorageInt _moneyStarsCounter;
        public IReadOnlySomeStorage<int> MoneyStarsCounter => _moneyStarsCounter;
        
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _moneyStarsCounter = new SomeStorageInt(int.MaxValue, 0);
            _pool = new Pool<MoneyStar>(MoneyStarInstantiate);
            _missionEventBus.Subscribe<EnemyStartDie>(this);
        }

        public override void GameCycleUpdate()
        {
            IReadOnlyList<IHandleUpdate> list = _pool.BusyElements;
            for (int i = 0; i < list.Count; i++)
                list[i].HandleUpdate(Time.deltaTime);
        }
        
        public void ApplyMoneyStars() => PlayerGlobalData.ChangeMoneyStarsCount(_moneyStarsCounter.CurrentValue);
        
        public void OnEvent(EnemyStartDie @event) => Spawn(@event.Position);

        private MoneyStar MoneyStarInstantiate()
        {
            var moneyStar = _moneyStarsFactory.Create(transform).GetComponentInChildren<MoneyStar>();
            moneyStar.OnStarTaking += OnMoneyStarTaking;
            moneyStar.OnLoseStar += ReturnStarInPool;
        
            return moneyStar;
        }

        private void Spawn(Vector3 position)
        {
            if (_pool.ExtractElement(out MoneyStar moneyStar))
                moneyStar.transform.position = position;
            else
                Debug.LogWarning("There was no extraction");
        }
        
        private void OnMoneyStarTaking(MoneyStar moneyStar)
        {

            _moneyStarsCounter.ChangeCurrentValue(1);
            ReturnStarInPool(moneyStar);
        }
        
        private void ReturnStarInPool(MoneyStar moneyStar) => _pool.ReturnElement(moneyStar);
    }
}
