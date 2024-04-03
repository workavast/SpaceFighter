using System.Collections.Generic;
using System.Linq;
using SpaceShips.Enemies;

namespace Managers
{
    public class EnemySpaceshipsUpdater
    {
        private readonly EnemySpaceshipsRepository _enemySpaceshipsRepository;
        
        public EnemySpaceshipsUpdater(EnemySpaceshipsRepository enemySpaceshipsRepository)
        {
            _enemySpaceshipsRepository = enemySpaceshipsRepository;
        }
        
        public void HandleUpdate(float time)
        {
            IReadOnlyList<EnemySpaceshipBase> list = _enemySpaceshipsRepository.Enemies.ToList();
            for (int j = 0; j < list.Count; j++)
                list[j].HandleUpdate(time);
        }
    }
}