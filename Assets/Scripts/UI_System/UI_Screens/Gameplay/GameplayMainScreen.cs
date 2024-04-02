using Controllers;
using Initializers;
using Managers;
using Saves;
using TMPro;
using UI_System.Elements;
using UnityEngine;
using Zenject;

namespace UI_System.UI_Screens.Gameplay
{
    public class GameplayMainScreen : UI_ScreenBase
    {
        [SerializeField] private TextMeshProUGUI coinsCounter;
        [SerializeField] private UI_Counter killsCounter;

        [SerializeField] private GameObject mainMenu;
        
        [Inject] private MissionController _missionController;
        [Inject] private CoinsManager _coinsManager;

        private void Awake()
        {
            mainMenu.SetActive(PlayerGlobalData.Instance.PlatformType == PlatformType.Desktop);
        }

        private void Start()
        {
            killsCounter.Init(_missionController.KillsCounter.DestroyedEnemiesCounter);
            
            _coinsManager.MoneyStarsCounter.OnChange += UpdateLevelMoneyCount;
            UpdateLevelMoneyCount();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
                _SetScreen(40);
        }

        private void UpdateLevelMoneyCount()
        {
            coinsCounter.text = $"{_coinsManager.MoneyStarsCounter.CurrentValue}";
        }
    }
}