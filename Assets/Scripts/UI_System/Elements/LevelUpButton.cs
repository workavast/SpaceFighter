using Localization;
using TMPro;
using UnityEngine;

namespace UI_System.Elements
{
    public class LevelUpButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private LocalizedPair maxLevel;
        [SerializeField] private LocalizedPair<int> levelUp;
        [SerializeField] private LocalizedPair<int> buy;

        private LocalizedPair _currentPair;
        
        public void Init()
        {
            maxLevel.Init();
            levelUp.Init();
            buy.Init();
            
            _currentPair = maxLevel;
        }
        
        public void SetMaxLevelText()
        {
            _currentPair.OnStringChange -= UpdateText;
            _currentPair = maxLevel;
            _currentPair.OnStringChange += UpdateText;
            
            UpdateText(maxLevel.Str);
        }

        public void SetLevelUpText(int price)
        {
            levelUp.SetValue(price);
            
            _currentPair.OnStringChange -= UpdateText;
            _currentPair = levelUp;
            _currentPair.OnStringChange += UpdateText;
            
            UpdateText(levelUp.Str);
        }
        
        public void SetBuyText(int price)
        {
            buy.SetValue(price);
            
            _currentPair.OnStringChange -= UpdateText;
            _currentPair = buy;
            _currentPair.OnStringChange += UpdateText;
            
            UpdateText(buy.Str);
        }

        private void UpdateText(string newStr) 
            => buttonText.text = newStr;
        
        private void OnDestroy()
        {
            maxLevel.Dispose();
            levelUp.Dispose();
            buy.Dispose();
        }
    }
}