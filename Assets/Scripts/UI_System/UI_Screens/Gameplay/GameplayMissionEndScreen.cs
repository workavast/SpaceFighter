using System;
using Controllers;
using Managers;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI_System.UI_Screens.Gameplay
{
    public class GameplayMissionEndScreen : UI_ScreenBase
    {
        [SerializeField] private TextMeshProUGUI missionCompleted;
        [SerializeField] private SuccessMark withoutDamage;
        [SerializeField] private TextMeshProUGUI kills;
        [SerializeField] private TextMeshProUGUI moneyStars;
        [SerializeField] private GameObject darkBackground;
        
        [Inject] private MissionController _missionController;
        [Inject] private MoneyStarsManager _moneyStarsManager;
        
        private void OnEnable()
        {
            if (_missionController.MissionStarsController.MissionSuccess)
            {
                darkBackground.SetActive(false);
                missionCompleted.text = "Mission success";
            }            
            else
            {
                darkBackground.SetActive(true);
                missionCompleted.text = "Mission loose";
            }            
            
            if(_missionController.MissionStarsController.PlayerTakeDamage)
                withoutDamage.UnSuccess();
            else
                withoutDamage.Success();
            
            kills.text = $"{_missionController.KillsCounter.DestroyedEnemiesCounter.CurrentValue}" +
                         $"/{_missionController.KillsCounter.DestroyedEnemiesCounter.MaxValue}";

            moneyStars.text = $"{_moneyStarsManager.MoneyStarsCounter.CurrentValue}";
        }

        [Serializable]
        private struct SuccessMark
        {
            [SerializeField] private GameObject successMark;
            [SerializeField] private GameObject unSuccessMark;

            public void Success()
            {
                successMark.SetActive(true);
                unSuccessMark.SetActive(false);
            }
            
            public void UnSuccess()
            {
                successMark.SetActive(false);
                unSuccessMark.SetActive(true);
            }
        }
    }
}