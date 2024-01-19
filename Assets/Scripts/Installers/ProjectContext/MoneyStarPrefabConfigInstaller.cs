using Configs;
using UnityEngine;
using Zenject;

namespace Installers.ProjectContext
{
    public class MoneyStarPrefabConfigInstaller : MonoInstaller
    {
        [SerializeField] private MoneyStarPrefabConfig moneyStarPrefabConfig;

        public override void InstallBindings()
        {
            Container.Bind<MoneyStarPrefabConfig>().FromInstance(moneyStarPrefabConfig).AsSingle();
        }
    }
}