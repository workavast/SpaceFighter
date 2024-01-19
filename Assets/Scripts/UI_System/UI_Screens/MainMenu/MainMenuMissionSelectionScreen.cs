using UI_System.Elements;
using UnityEngine;
using Zenject;

namespace UI_System.UI_Screens.MainMenu
{
    public class MainMenuMissionSelectionScreen : UI_ScreenBase
    {
        [SerializeField] private MissionSelectionButton[] missionButtons;
        
        [Inject] private SelectedMissionData _selectedMissionData;

        private void Awake()
        {
            foreach (var button in missionButtons)
                button.OnLoadMission += SelectMission;
        }

        private void SelectMission(int missionIndex)
        {
            _selectedMissionData.SetMissionData(missionIndex);
            _LoadScene(2);
        }
    }
}