using System;
using System.Collections;
using System.Collections.Generic;
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
