using System.Collections.Generic;
using Factories;
using UnityEngine;

namespace Managers
{
    public class CoinsRepository
    {
        private readonly List<Coin> _coins = new();
        private readonly CoinsFactory _coinsFactory;
        
        public IReadOnlyList<Coin> Coins => _coins;
        
        public CoinsRepository(CoinsFactory coinsFactory)
        {
            _coinsFactory = coinsFactory;
            _coinsFactory.OnCreate += Add;
        }
        
        private void Add(Coin newCoin)
        {
            if (_coins.Contains(newCoin))
            {
                Debug.LogWarning($"Coin duplicate");
                return;
            }
            
            _coins.Add(newCoin);
            newCoin.ReturnElementEvent += Remove;
        }

        private void Remove(Coin coin)
        {
            if (!_coins.Remove(coin))
                Debug.LogWarning($"Repository dont contain coin");
            else
                coin.ReturnElementEvent -= Remove;
        }
    }
}