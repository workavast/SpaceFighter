using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class SelectedMissionDataInstaller : MonoInstaller
    {
        [SerializeField] private SelectedMissionData selectedMissionData;
        
        public override void InstallBindings()
        {
            Container.Bind<SelectedMissionData>().FromInstance(selectedMissionData).AsSingle();
        }
    }
}