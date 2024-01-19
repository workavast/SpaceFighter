using Managers;
using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class ManagersInstaller : MonoInstaller
    {
        [SerializeField] private ManagerBase[] managers;

        public override void InstallBindings()
        {
            foreach (var manager in managers)
                Container.Bind(manager.GetType()).FromInstance(manager).AsSingle();
        }
    }
}