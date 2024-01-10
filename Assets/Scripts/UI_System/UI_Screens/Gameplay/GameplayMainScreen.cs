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
    
    private void Start()
    {
        killsCounter.Init(_missionController.DestroyedEnemiesCounter);
        wavesCounter.Init(_missionController.WavesCounter);
    }
    
    public void UpdateLevelMoneyCount(int currentCount)
    {
        levelMoneyCounter.text = $"{currentCount}";
    }
}