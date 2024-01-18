using Controllers;
using Managers;
using TMPro;
using UI_System.Elements;
using UnityEngine;
using Zenject;

namespace UI_System.UI_Screens.Gameplay
{
    public class GameplayMainScreen : UI_ScreenBase
    {
        [SerializeField] private TextMeshProUGUI levelMoneyCounter;
        [SerializeField] private UI_Counter wavesCounter;
        [SerializeField] private UI_Counter killsCounter;

        [Inject] private MissionController _missionController;
        [Inject] private WavesManager _wavesManager;
        [Inject] private MoneyStarsManager _moneyStarsManager;
        
        private void Start()
        {
            killsCounter.Init(_missionController.KillsCounter.DestroyedEnemiesCounter);
            wavesCounter.Init(_wavesManager.WavesCounter);
            
            _moneyStarsManager.MoneyStarsCounter.OnChange += UpdateLevelMoneyCount;
            UpdateLevelMoneyCount();
        }
    
        private void UpdateLevelMoneyCount()
        {
            levelMoneyCounter.text = $"{_moneyStarsManager.MoneyStarsCounter.CurrentValue}";
        }
    }
}