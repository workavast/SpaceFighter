using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class MobileInputDetectorInstaller : MonoInstaller
    {
        [SerializeField] private MobileInputDetector mobileInputDetector;
        
        public override void InstallBindings()
        {
            Container.Bind<MobileInputDetector>().FromInstance(mobileInputDetector).AsSingle();
        }
    }
}