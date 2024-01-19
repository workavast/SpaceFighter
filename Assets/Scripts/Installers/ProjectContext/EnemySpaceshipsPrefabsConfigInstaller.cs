using Configs;
using UnityEngine;
using Zenject;

namespace Installers.ProjectContext
{
    public class EnemySpaceshipsPrefabsConfigInstaller : MonoInstaller
    {
        [SerializeField] private EnemySpaceshipsPrefabsConfig enemySpaceshipsPrefabsConfig;
    
        public override void InstallBindings()
        {
            Container.Bind<EnemySpaceshipsPrefabsConfig>().FromInstance(enemySpaceshipsPrefabsConfig).AsSingle();
        }
    }
}
