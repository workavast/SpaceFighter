using System;
using Configs;
using PoolSystem;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class CoinsFactory : FactoryBase
    {
        [Inject] private readonly CoinPrefabConfig _prefabConfig;
        [Inject] private readonly DiContainer _diContainer;

        private Pool<Coin> _pool;
        
        private GameObject CoinPrefab => _prefabConfig.Data;
        
        public event Action<Coin> OnCreate;

        private void Awake()
        {
            _pool = new Pool<Coin>(CoinInstantiate);
        }
        
        public Coin Create(Vector3 position)
        {
            if(!_pool.ExtractElement(out var coin))
                throw new Exception($"coin wasn't extract from pool");

            coin.transform.position = position;
            OnCreate?.Invoke(coin);
            return coin;
        }
        
        private Coin CoinInstantiate()
        {
            var coin = _diContainer.InstantiatePrefab(CoinPrefab, transform).GetComponent<Coin>();
            
            return coin;
        }
    }
}
