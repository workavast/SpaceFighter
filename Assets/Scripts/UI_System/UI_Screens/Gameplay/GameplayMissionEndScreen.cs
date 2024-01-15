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
        [SerializeField] private StarsBlock starsBlock;
        
        [Inject] private MissionController _missionController;
        [Inject] private MoneyStarsManager _moneyStarsManager;
        
        private void OnEnable()
        {
            if (_missionController.StarsController.MissionSuccess)
            {
                darkBackground.SetActive(false);
                missionCompleted.text = "Mission success";
            }            
            else
            {
                darkBackground.SetActive(true);
                missionCompleted.text = "Mission loose";
            }            
            
            if(_missionController.StarsController.PlayerTakeDamage)
                withoutDamage.UnSuccess();
            else
                withoutDamage.Success();
            
            kills.text = $"{_missionController.KillsCounter.DestroyedEnemiesCounter.CurrentValue}" +
                         $"/{_missionController.KillsCounter.DestroyedEnemiesCounter.MaxValue}";

            moneyStars.text = $"{_moneyStarsManager.MoneyStarsCounter.CurrentValue}";
            
            starsBlock.ShowStars(_missionController.StarsController.StarsCount);
        }

        [Serializable]
        private class StarsBlock
        {
            [SerializeField] private GameObject[] earnedStars;

            public void ShowStars(int starsCount)
            {
#if UNITY_EDITOR
                if(starsCount < 0 || starsCount > earnedStars.Length)
                    Debug.LogError($"starsCount[{starsCount}] | earnedStars.Length[{earnedStars.Length}]");
#endif

                foreach (var star in earnedStars)
                    star.SetActive(false);

                if (starsCount < 0) starsCount = 0;
                var i = starsCount >= earnedStars.Length 
                        ? earnedStars.Length 
                        : starsCount;

                for (int j = 0; j < i; j++)
                    earnedStars[j].SetActive(true);
            }
        }
        
        [Serializable]
        private class SuccessMark
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