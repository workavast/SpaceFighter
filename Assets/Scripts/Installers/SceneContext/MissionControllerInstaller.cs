using Managers;
using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class MissionControllerInstaller : MonoInstaller
    {
        [SerializeField] private MissionController missionController;
        
        public override void InstallBindings()
        {
            Container.Bind<MissionController>().FromInstance(missionController).AsSingle();
        }
    }
}