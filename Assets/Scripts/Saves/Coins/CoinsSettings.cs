using Saves.Savers;

namespace Saves.Coins
{
    public class CoinsSettings : SettingsBase<CoinsData, CoinsSave>
    {
        protected override string SaveKey => "CoinsSettings";
        
        public int CoinsCount => Data.CoinsCount;

        public CoinsSettings(ISaver saver) : base(saver) { }
        
        public void ChangeCoinsCount(int changeValue)
        {
            Data.CoinsCount += changeValue;
            Save();
        }
    }
}