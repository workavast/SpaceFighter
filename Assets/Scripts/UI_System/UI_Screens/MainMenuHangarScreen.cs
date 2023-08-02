using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuHangarScreen : UI_ScreenBase
{
    [SerializeField] private TextMeshProUGUI moneyStarsCount;

    private void Start()
    {
        UpdateMoneyStarsCount();
    }

    private void UpdateMoneyStarsCount()
    {
        moneyStarsCount.text = "" + PlayerGlobalData.MoneyStarsCount;
    }
}
