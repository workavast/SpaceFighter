using Configs.Missions;
using UnityEngine;
using Zenject;

public class MissionsConfigInstaller : MonoInstaller
{
    [SerializeField] private MissionsConfig missionsConfig;

    public override void InstallBindings()
    {
        Container.Bind<MissionsConfig>().FromInstance(missionsConfig).AsSingle();
    }
}
