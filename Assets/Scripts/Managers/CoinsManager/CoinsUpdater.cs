using System.Collections.Generic;
using System.Linq;
using Projectiles.Enemy;

namespace Managers
{
    public class CoinsUpdater
    {
        private readonly CoinsRepository _repository;
        
        public CoinsUpdater(CoinsRepository repository)
        {
            _repository = repository;
        }
        
        public void HandleUpdate(float time)
        {
            IReadOnlyList<Coin> list = _repository.Coins.ToList();
            for (int j = 0; j < list.Count; j++)
                list[j].HandleUpdate(time);
        }
    }
}