using Zenject;

public class MainMenuHangarScreenInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MainMenuHangarScreen>().FromInstance(FindObjectOfType<MainMenuHangarScreen>(true)).AsSingle();
    }
}
