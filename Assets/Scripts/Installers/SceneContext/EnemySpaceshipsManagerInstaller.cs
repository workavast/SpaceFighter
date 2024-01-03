using Managers;
using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class EnemySpaceshipsManagerInstaller : MonoInstaller
    {
        [SerializeField] private EnemySpaceshipsManager manager;
        
        public override void InstallBindings()
        {
            Container.Bind<EnemySpaceshipsManager>().FromInstance(manager).AsSingle();
        }
    }
}