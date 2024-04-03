using System.Collections.Generic;
using System.Linq;
using Projectiles.Enemy;

namespace Managers
{
    public class EnemyProjectilesUpdater
    {
        private readonly EnemyProjectilesRepository _enemyProjectilesRepository;
        
        public EnemyProjectilesUpdater(EnemyProjectilesRepository enemyProjectilesRepository)
        {
            _enemyProjectilesRepository = enemyProjectilesRepository;
        }

        public void GameCycleEnter()
        {
            IReadOnlyList<EnemyProjectileBase> list = _enemyProjectilesRepository.Projectiles;
            for (int j = 0; j < list.Count; j++)
                list[j].GameCycleStateEnter();
        }
        
        public void GameCycleExit()
        {
            IReadOnlyList<EnemyProjectileBase> list = _enemyProjectilesRepository.Projectiles;
            for (int j = 0; j < list.Count; j++)
                list[j].GameCycleStateExit();
        }
        
        public void HandleUpdate(float time)
        {
            IReadOnlyList<EnemyProjectileBase> list = _enemyProjectilesRepository.Projectiles.ToList();
            for (int j = 0; j < list.Count; j++)
                list[j].HandleUpdate(time);
        }
    }
}