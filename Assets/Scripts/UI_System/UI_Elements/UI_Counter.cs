using System;
using SomeStorages;
using TMPro;
using UnityEngine;

namespace UI_System.UI_Elements
{
    [Serializable]
    public class UI_Counter
    {
        [SerializeField] private TextMeshProUGUI counter;

        private IReadOnlySomeStorage<int> _counter;

        public void Init(IReadOnlySomeStorage<int> someStorage)
        {
            _counter = someStorage;
            someStorage.OnChange += UpdateCounter;
            UpdateCounter();
        }
    
        private void UpdateCounter() => counter.text = $"{_counter.CurrentValue}/{_counter.MaxValue}";
    }
}