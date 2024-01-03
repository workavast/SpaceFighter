using Managers;
using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class WavesManagerInstaller: MonoInstaller
    {
        [SerializeField] private WavesManager manager;
        
        public override void InstallBindings()
        {
            Container.Bind<WavesManager>().FromInstance(manager).AsSingle();
        }
    }
}