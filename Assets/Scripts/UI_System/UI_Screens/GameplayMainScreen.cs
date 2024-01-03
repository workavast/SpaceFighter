using TMPro;
using UnityEngine;

public class GameplayMainScreen : UI_ScreenBase
{
    [SerializeField] private TextMeshProUGUI levelMoneyCount;
    
    public void UpdateLevelMoneyCount(int currentCount)
    {
        levelMoneyCount.text = "" + currentCount;
    }
}
