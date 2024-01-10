using Configs;
using UnityEngine;
using Zenject;

public class EnemyProjectilesPrefabsConfigInstaller : MonoInstaller
{
    [SerializeField] private EnemyProjectilesPrefabsConfig enemyProjectilesPrefabsConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<EnemyProjectilesPrefabsConfig>().FromInstance(enemyProjectilesPrefabsConfig).AsSingle();
    }
}
