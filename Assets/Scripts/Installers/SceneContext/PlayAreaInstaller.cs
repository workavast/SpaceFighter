using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class PlayAreaInstaller : MonoInstaller
    {
        [SerializeField] private PlayArea playArea;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayArea>().FromInstance(playArea).AsSingle();
        }
    }
}