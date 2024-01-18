using GameCycle;
using UnityEngine;
using Zenject;

public class GameCycleManagerInstaller : MonoInstaller
{
    [SerializeField] private GameCycleController gameCycleController;

    public override void InstallBindings()
    {
        Container.Bind<IGameCycleController>().FromInstance(gameCycleController).AsSingle();
        Container.Bind<IGameCycleSwitcher>().FromInstance(gameCycleController).AsSingle();
    }
}
