using UI_System;
using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class UIControllerInstaller : MonoInstaller
    {
        [SerializeField] private UI_Controller uiController;

        public override void InstallBindings()
        {
            Container.Bind<UI_Controller>().FromInstance(uiController).AsSingle();
        }
    }
}
