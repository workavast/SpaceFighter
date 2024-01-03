using MissionsDataConfigsSystem;

public class MainMenuMissionSelectionScreen : UI_ScreenBase
{
    public void _SelectMission(int missionNum)
    {
        SelectedMissionData.SetMissionData(missionNum-1);
        _LoadScene(1);
    }
}
