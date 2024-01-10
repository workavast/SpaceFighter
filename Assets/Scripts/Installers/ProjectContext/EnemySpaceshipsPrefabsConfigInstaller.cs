using Configs;
using UnityEngine;
using Zenject;

public class EnemySpaceshipsPrefabsConfigInstaller : MonoInstaller
{
    [SerializeField] private EnemySpaceshipsPrefabsConfig enemySpaceshipsPrefabsConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<EnemySpaceshipsPrefabsConfig>().FromInstance(enemySpaceshipsPrefabsConfig).AsSingle();
    }
}
