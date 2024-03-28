using System;

namespace Saves.Coins
{
    public class CoinsSettings : ISettings
    {
        public int CoinsCount;
        public event Action OnChange;

        public CoinsSettings()
        {
            CoinsCount = 1000;
        }
        
        public void SetData(CoinsSettingsSave settingsSave)
        {
            CoinsCount = settingsSave.CoinsCount;
        }        
        
        public void ChangeCoinsCount(int changeValue)
        {
            CoinsCount += changeValue;
            OnChange?.Invoke();
        }
    }
}