using System.Collections.Generic;
using System.Linq;
using Projectiles.Player;

namespace Managers
{
    public class PlayerProjectilesUpdater
    {
        private readonly PlayerProjectilesRepository _enemyProjectilesRepository;
        
        public PlayerProjectilesUpdater(PlayerProjectilesRepository enemyProjectilesRepository)
        {
            _enemyProjectilesRepository = enemyProjectilesRepository;
        }

        public void GameCycleEnter()
        {
            IReadOnlyList<PlayerProjectileBase> list = _enemyProjectilesRepository.Projectiles;
            for (int j = 0; j < list.Count; j++)
                list[j].GameCycleStateEnter();
        }
        
        public void GameCycleExit()
        {
            IReadOnlyList<PlayerProjectileBase> list = _enemyProjectilesRepository.Projectiles;
            for (int j = 0; j < list.Count; j++)
                list[j].GameCycleStateExit();
        }
        
        public void HandleUpdate(float time)
        {
            IReadOnlyList<PlayerProjectileBase> list = _enemyProjectilesRepository.Projectiles.ToList();
            for (int j = 0; j < list.Count; j++)
                list[j].HandleUpdate(time);
        }
    }
}