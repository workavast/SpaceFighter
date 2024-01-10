using Configs.Missions;
using UnityEngine;
using Zenject;
using MissionsDataConfigsSystem;

public class MissionsConfigInstaller : MonoInstaller
{
    [SerializeField] private MissionsConfig missionsConfig;

    public override void InstallBindings()
    {
        Container.Bind<MissionsConfig>().FromInstance(missionsConfig).AsSingle();
    }
}
