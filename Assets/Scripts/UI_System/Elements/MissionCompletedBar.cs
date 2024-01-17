using Localization;
using TMPro;
using UnityEngine;

namespace UI_System.Elements
{
    public class MissionCompletedBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textFill;
        
        [SerializeField] private LocalizedPair missionCompleted;
        [SerializeField] private LocalizedPair missionLoose;

        private LocalizedPair _currentPair;
        
        public void Init()
        {
            missionCompleted.Init();
            missionLoose.Init();

            _currentPair = missionCompleted;
        }
        
        public void SetMissionCompletedText() => UpdateText(missionCompleted);
        public void SetMissionLooseText() => UpdateText(missionLoose);
        
        private void UpdateText(LocalizedPair newLocalizedPair)
        {
            _currentPair.OnStringChange -= UpdateText;
            _currentPair = newLocalizedPair;
            _currentPair.OnStringChange += UpdateText;
            
            UpdateText(newLocalizedPair.Str);
        }
        
        private void UpdateText(string newStr) => textFill.text = newStr;
        
        private void OnDestroy()
        {
            missionCompleted.Dispose();
            missionLoose.Dispose();
        }
    }
}