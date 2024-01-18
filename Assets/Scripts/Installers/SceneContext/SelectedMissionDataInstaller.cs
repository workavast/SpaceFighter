using UnityEngine;
using Zenject;

namespace Installers.SceneContext
{
    public class SelectedMissionDataInstaller : MonoInstaller
    {
        [SerializeField] private SelectedMissionData.SelectedMissionData selectedMissionData;
        
        public override void InstallBindings()
        {
            Container.Bind<SelectedMissionData.SelectedMissionData>().FromInstance(selectedMissionData).AsSingle();
        }
    }
}