using Factories;
using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private FactoryBase[] factories;

        public override void InstallBindings()
        {
            foreach (var factory in factories)
                Container.Bind(factory.GetType()).FromInstance(factory).AsSingle();
        }
    }
}