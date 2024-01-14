using Controllers;
using Managers;
using TMPro;
using UI_System.UI_Elements;
using UnityEngine;
using Zenject;

public class GameplayMainScreen : UI_ScreenBase
{
    [SerializeField] private TextMeshProUGUI levelMoneyCounter;
    [SerializeField] private UI_Counter wavesCounter;
    [SerializeField] private UI_Counter killsCounter;

    [Inject] private MissionController _missionController;

    [Inject] private KillsCounterController _killsCounterController;
    [Inject] private WavesController _wavesController;
    
    private void Start()
    {
        killsCounter.Init(_killsCounterController.DestroyedEnemiesCounter);
        wavesCounter.Init(_wavesController.WavesCounter);
    }
    
    public void UpdateLevelMoneyCount(int currentCount)
    {
        levelMoneyCounter.text = $"{currentCount}";
    }
}