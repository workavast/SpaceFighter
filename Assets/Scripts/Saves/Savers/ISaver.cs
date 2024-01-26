namespace Saves.Savers
{
    public interface ISaver
    {
        public void Save<TData, TSave>(string saveKey, TData data)
            where TData : DataBase<TSave>
            where TSave : SaveBase<TData>, new();

        public TData Load<TData, TSave>(string saveKey)
            where TData : DataBase<TSave>, new()
            where TSave : SaveBase<TData>;
    }
}