using Configs;
using UnityEngine;
using Zenject;

namespace Installers.ProjectContext
{
    public class CoinPrefabConfigInstaller : MonoInstaller
    {
        [SerializeField] private CoinPrefabConfig coinPrefabConfig;

        public override void InstallBindings()
        {
            Container.Bind<CoinPrefabConfig>().FromInstance(coinPrefabConfig).AsSingle();
        }
    }
}