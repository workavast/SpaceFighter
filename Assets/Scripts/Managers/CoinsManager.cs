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
        [SerializeField] private int starsPerCoin;
        [SerializeField] private int starsPerCoinOffset;
        [SerializeField] [Range(0, 1)] private float coinDropChance;
        
        [Inject] private CoinsFactory _coinsFactory;
        [Inject] private EventBus _eventBus;
        
        public EventBusReceiverIdentifier EventBusReceiverIdentifier { get; } = new();
        public IReadOnlySomeStorage<int> MoneyStarsCounter => _moneyStarsCounter;
        
        protected override GameCycleState GameCycleState => GameCycleState.Gameplay;
        
        private SomeStorageInt _moneyStarsCounter;
        private Pool<Coin> _pool;
        
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
        
        public void ApplyMoneyStars() 
            => PlayerGlobalData.Instance.CoinsSettings.ChangeCoinsCount(_moneyStarsCounter.CurrentValue);
        
        public void OnEvent(EnemyStartDie @event)
        {
            var random = Random.value;
            if(random <= coinDropChance)
                Spawn(@event.Position);
        }

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
            var offset = Random.Range(-starsPerCoinOffset, starsPerCoinOffset);
            _moneyStarsCounter.ChangeCurrentValue(starsPerCoin + offset);
            _eventBus.Invoke(new CoinPickUp());
            ReturnCoinInPool(coin);
        }
        
        private void ReturnCoinInPool(Coin coin) 
            => _pool.ReturnElement(coin);
    }
}
