using System.Collections.Generic;
using EventBusEvents;
using EventBusExtension;
using Factories;
using GameCycle;
using PoolSystem;
using Saves;
using SomeStorages;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class CoinsManager : GameCycleManager, IEventReceiver<EnemyStartDie>
    {
        protected override GameCycleState GameCycleState => GameCycleState.Gameplay;

        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();
   
        [Inject] private CoinsFactory _coinsFactory;
        [Inject] private EventBus _eventBus;
        
        private Pool<Coin> _pool;

        private SomeStorageInt _moneyStarsCounter;
        public IReadOnlySomeStorage<int> MoneyStarsCounter => _moneyStarsCounter;
        
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _moneyStarsCounter = new SomeStorageInt(int.MaxValue, 0);
            _pool = new Pool<Coin>(CoinInstantiate);
            _eventBus.Subscribe<EnemyStartDie>(this);
        }

        public override void GameCycleUpdate()
        {
            IReadOnlyList<IHandleUpdate> list = _pool.BusyElements;
            for (int i = 0; i < list.Count; i++)
                list[i].HandleUpdate(Time.deltaTime);
        }
        
        public void ApplyMoneyStars() => PlayerGlobalData.Instance.CoinsSettings.ChangeCoinsCount(_moneyStarsCounter.CurrentValue);
        
        public void OnEvent(EnemyStartDie @event) => Spawn(@event.Position);

        private Coin CoinInstantiate()
        {
            var coin = _coinsFactory.Create(transform).GetComponentInChildren<Coin>();
            coin.OnPickUp += OnCoinPickUp;
            coin.OnLose += ReturnCoinInPool;
        
            return coin;
        }

        private void Spawn(Vector3 position)
        {
            if (_pool.ExtractElement(out Coin moneyStar))
                moneyStar.transform.position = position;
            else
                Debug.LogWarning("There was no extraction");
        }
        
        private void OnCoinPickUp(Coin coin)
        {
            _moneyStarsCounter.ChangeCurrentValue(1);
            _eventBus.Invoke(new CoinPickUp());
            ReturnCoinInPool(coin);
        }
        
        private void ReturnCoinInPool(Coin coin) => _pool.ReturnElement(coin);
    }
}
