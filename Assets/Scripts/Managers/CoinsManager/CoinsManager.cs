using EventBusEvents;
using EventBusExtension;
using Factories;
using GameCycle;
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
        private CoinsRepository _coinsRepository;
        private CoinsUpdater _coinsUpdater;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _moneyStarsCounter = new SomeStorageInt(int.MaxValue, 0);
            _coinsRepository = new CoinsRepository(_coinsFactory);
            _coinsUpdater = new CoinsUpdater(_coinsRepository);
            
            _eventBus.Subscribe<EnemyStartDie>(this);
        }

        public override void GameCycleUpdate()
            => _coinsUpdater.HandleUpdate(Time.deltaTime);
        
        public void ApplyMoneyStars() 
            => PlayerGlobalData.Instance.CoinsSettings.ChangeCoinsCount(_moneyStarsCounter.CurrentValue);
        
        public void OnEvent(EnemyStartDie @event)
        {
            var random = Random.value;
            if (random <= coinDropChance)
            {
                var coin = _coinsFactory.Create(@event.Position);
                coin.OnPickUp += OnCoinPickUp;
                coin.ReturnElementEvent += OnReturnCoinInPool;
            }
        }
        
        private void OnCoinPickUp()
        {
            var offset = Random.Range(-starsPerCoinOffset, starsPerCoinOffset);
            _moneyStarsCounter.ChangeCurrentValue(starsPerCoin + offset);
            _eventBus.Invoke(new CoinPickUp());
        }

        private void OnReturnCoinInPool(Coin coin)
        {
            coin.OnPickUp -= OnCoinPickUp;
            coin.ReturnElementEvent -= OnReturnCoinInPool;
        }
    }
}
