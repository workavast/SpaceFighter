using Localization;
using TMPro;
using UnityEngine;

namespace UI_System.Elements
{
    public class EquipButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private LocalizedPair equip;
        [SerializeField] private LocalizedPair equipped;
        [SerializeField] private LocalizedPair locked;
        
        private LocalizedPair _currentPair;
        
        public void Init()
        { 
            equip.Init();
            equipped.Init();
            locked.Init();
            
            _currentPair = equip;
        }
    
        public void SetEquipText() => UpdateText(equip);
        public void SetEquippedText() => UpdateText(equipped);
        public void SetLockedText() => UpdateText(locked);
        
        private void UpdateText(LocalizedPair newLocalizedPair)
        {
            _currentPair.OnStringChange -= UpdateText;
            _currentPair = newLocalizedPair;
            _currentPair.OnStringChange += UpdateText;
            
            UpdateText(newLocalizedPair.Str);
        }
        
        private void UpdateText(string newStr) => buttonText.text = newStr;
        
        private void OnDestroy()
        {
            equip.Dispose();
            equipped.Dispose();
            locked.Dispose();
        }
    }
}
