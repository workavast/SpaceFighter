using UnityEngine;
using Zenject;

public class MoneyStarPrefabConfigInstaller : MonoInstaller
{
    [SerializeField] private MoneyStarPrefabConfig moneyStarPrefabConfig;

    public override void InstallBindings()
    {
        Container.Bind<MoneyStarPrefabConfig>().FromInstance(moneyStarPrefabConfig).AsSingle();
    }
}