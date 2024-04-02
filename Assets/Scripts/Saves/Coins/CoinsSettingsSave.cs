using System;

namespace Saves.Coins
{
    [Serializable]
    public sealed class CoinsSettingsSave
    {
        public int CoinsCount = 0;

        public CoinsSettingsSave()
        {
            CoinsCount = 0;
        }
        
        public CoinsSettingsSave(CoinsSettings settings)
        {   
            CoinsCount = settings.CoinsCount;
        }        
    }
}