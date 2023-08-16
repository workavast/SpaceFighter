using UnityEngine;
using Zenject;
using MissionsDataConfigsSystem;
using UnityEngine.Serialization;

public class MissionsDataConfigInstaller : MonoInstaller
{
    [SerializeField] private MissionsDataConfig missionsDataConfig;

    public override void InstallBindings()
    {
        Container.Bind<MissionsDataConfig>().FromInstance(missionsDataConfig).AsSingle();
    }
}
