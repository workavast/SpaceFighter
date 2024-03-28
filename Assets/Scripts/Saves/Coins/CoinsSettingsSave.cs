using System;

namespace Saves.Coins
{
    [Serializable]
    public sealed class CoinsSettingsSave
    {
        public int CoinsCount;

        public CoinsSettingsSave(CoinsSettings settings)
        {   
            CoinsCount = settings.CoinsCount;
        }        
    }
}