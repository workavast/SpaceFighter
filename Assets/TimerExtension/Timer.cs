using System;
using SomeStorages;

namespace TimerExtension
{
    public class Timer
    {
        private SomeStorageFloat _timer;
        
        public IReadOnlySomeStorage<float> TimerValues => _timer;
        public bool TimerEnd { get; private set; }

        public event Action OnTimerEnd;

        public Timer(float startValue)
        {
            _timer = new SomeStorageFloat(startValue);
        }
        
        public void SetTimer(float newMaxValue, bool saveCurrentValue = false)
        {
            _timer = saveCurrentValue
                ? new SomeStorageFloat(newMaxValue, _timer.CurrentValue)
                : new SomeStorageFloat(newMaxValue);
        }

        public void Reset()
        {
            TimerEnd = false;
            _timer.SetCurrentValue(0);
        }

        public void Tick(float time)
        {
            if(TimerEnd) return;
            
            UpdateTimer(time);
        }

        private void UpdateTimer(float time)
        {
            _timer.ChangeCurrentValue(time);
            
            if (_timer.IsFull)
            {
                TimerEnd = true;
                OnTimerEnd?.Invoke();
            }
        }
    }
}