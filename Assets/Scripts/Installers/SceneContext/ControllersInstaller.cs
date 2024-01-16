using Controllers;
using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class ControllersInstaller : MonoInstaller
    {
        [SerializeField] private ControllerBase[] controllers;
        
        public override void InstallBindings()
        {
            foreach (var controller in controllers)
                Container.Bind(controller.GetType()).FromInstance(controller).AsSingle();
        }
    }
}