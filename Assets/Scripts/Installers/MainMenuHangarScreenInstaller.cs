using UnityEngine;
using Zenject;

public class MainMenuHangarScreenInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MainMenuHangarScreen>().FromInstance(FindObjectOfType<MainMenuHangarScreen>()).AsSingle();
    }
}
