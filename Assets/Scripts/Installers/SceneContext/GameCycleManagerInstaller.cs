using GameCycle;
using UnityEngine;
using Zenject;

public class GameCycleManagerInstaller : MonoInstaller
{
    [SerializeField] private GameCycleManager gameCycleManager;

    public override void InstallBindings()
    {
        Container.Bind<IGameCycleManager>().FromInstance(gameCycleManager).AsSingle();
    }
}
