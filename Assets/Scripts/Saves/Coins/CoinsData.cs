namespace Saves.Coins
{
    public class CoinsData : DataBase<CoinsSave>
    {
        public int CoinsCount;

        public CoinsData()
        {
            CoinsCount = 1000;
        }
        
        public override void SetData(CoinsSave save)
        {
            CoinsCount = save.CoinsCount;
        }
    }
}