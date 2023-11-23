using System.Collections;
using System.Collections.Generic;
using MissionsDataConfigsSystem;
using UnityEngine;

public class MainMenuMissionSelectionScreen : UI_ScreenBase
{
    public void _SelectMission(int missionNum)
    {
        SelectedMissionData.SetMissionData(missionNum-1);
        _LoadScene(1);
    }
}
