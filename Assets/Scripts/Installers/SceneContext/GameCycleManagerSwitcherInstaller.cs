using GameCycle;
using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class GameCycleManagerSwitcherInstaller : MonoInstaller
    {
        [SerializeField] private GameCycleManager gameCycleManager;
        
        public override void InstallBindings()
        {
            Container.Bind<IGameCycleManagerSwitcher>().FromInstance(gameCycleManager).AsSingle();
        }
    }
}