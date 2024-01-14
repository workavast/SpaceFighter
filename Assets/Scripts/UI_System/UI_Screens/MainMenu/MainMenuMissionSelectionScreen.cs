using MissionsDataConfigsSystem;
using Zenject;

namespace UI_System.UI_Screens.MainMenu
{
    public class MainMenuMissionSelectionScreen : UI_ScreenBase
    {
        [Inject] private SelectedMissionData _selectedMissionData;
        
        public void _SelectMission(int missionNum)
        {
            _selectedMissionData.SetMissionData(missionNum-1);
            _LoadScene(1);
        }
    }
}