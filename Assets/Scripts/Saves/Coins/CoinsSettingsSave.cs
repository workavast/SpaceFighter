using System;

namespace Saves.Coins
{
    [Serializable]
    public sealed class CoinsSettingsSave
    {
        public int CoinsCount = 1000;

        public CoinsSettingsSave()
        {
            CoinsCount = 1000;
        }
        
        public CoinsSettingsSave(CoinsSettings settings)
        {   
            CoinsCount = settings.CoinsCount;
        }        
    }
}