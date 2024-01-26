namespace Saves.Coins
{
    public class CoinsSave : SaveBase<CoinsData>
    {
        public int CoinsCount;
        
        public override void SetData(CoinsData data)
        {
            CoinsCount = data.CoinsCount;
        }
    }
}