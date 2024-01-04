using UnityEngine;
using Zenject;

public class UIControllerInstaller : MonoInstaller
{
    [SerializeField] private UI_Controller uiController;

    public override void InstallBindings()
    {
        Container.Bind<UI_Controller>().FromInstance(uiController).AsSingle();
    }
}
