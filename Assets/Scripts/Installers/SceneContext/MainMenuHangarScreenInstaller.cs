using UI_System.UI_Screens.MainMenu;
using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class MainMenuHangarScreenInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuHangarScreen mainMenuHangarScreen;
        
        public override void InstallBindings()
        {
            Container.Bind<MainMenuHangarScreen>().FromInstance(mainMenuHangarScreen).AsSingle();
        }
    }
}
