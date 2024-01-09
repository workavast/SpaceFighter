using UnityEngine;
using Zenject;

public class MainMenuHangarScreenInstaller : MonoInstaller
{
    [SerializeField] private MainMenuHangarScreen mainMenuHangarScreen;
    public override void InstallBindings()
    {
        Container.Bind<MainMenuHangarScreen>().FromInstance(mainMenuHangarScreen).AsSingle();
    }
}
