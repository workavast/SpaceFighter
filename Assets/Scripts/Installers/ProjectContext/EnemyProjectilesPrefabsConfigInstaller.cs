using Configs;
using UnityEngine;
using Zenject;

namespace Installers.ProjectContext
{
    public class EnemyProjectilesPrefabsConfigInstaller : MonoInstaller
    {
        [SerializeField] private EnemyProjectilesPrefabsConfig enemyProjectilesPrefabsConfig;
    
        public override void InstallBindings()
        {
            Container.Bind<EnemyProjectilesPrefabsConfig>().FromInstance(enemyProjectilesPrefabsConfig).AsSingle();
        }
    }
}
