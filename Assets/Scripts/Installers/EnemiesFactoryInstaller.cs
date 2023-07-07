using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesFactoryInstaller : MonoInstaller
{
    [SerializeField] private EnemiesSpaceshipsPrefabsConfig enemiesSpaceshipsPrefabsConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<EnemiesSpaceshipsPrefabsConfig>().FromInstance(enemiesSpaceshipsPrefabsConfig).AsSingle();
    }
}
