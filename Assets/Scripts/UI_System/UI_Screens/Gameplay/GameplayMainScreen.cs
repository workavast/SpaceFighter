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
        [SerializeField] private TextMeshProUGUI coinsCounter;
        [SerializeField] private UI_Counter wavesCounter;
        [SerializeField] private UI_Counter killsCounter;

        [Inject] private MissionController _missionController;
        [Inject] private WavesManager _wavesManager;
        [Inject] private CoinsManager _coinsManager;
        
        private void Start()
        {
            killsCounter.Init(_missionController.KillsCounter.DestroyedEnemiesCounter);
            wavesCounter.Init(_wavesManager.WavesCounter);
            
            _coinsManager.MoneyStarsCounter.OnChange += UpdateLevelMoneyCount;
            UpdateLevelMoneyCount();
        }
    
        private void UpdateLevelMoneyCount()
        {
            coinsCounter.text = $"{_coinsManager.MoneyStarsCounter.CurrentValue}";
        }
    }
}