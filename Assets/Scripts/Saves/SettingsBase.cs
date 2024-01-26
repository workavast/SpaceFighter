using Saves.Savers;

namespace Saves
{
    public abstract class SettingsBase<TData, TSave>
        where TData : DataBase<TSave>, new()
        where TSave : SaveBase<TData>, new()
    {
        protected abstract string SaveKey { get; }
        private ISaver Saver { get; }

        protected TData Data;

        protected SettingsBase(ISaver saver) => Saver = saver;
        
        public void Load() => Data = Saver.Load<TData, TSave>(SaveKey);
        
        protected void Save() => Saver.Save<TData, TSave>(SaveKey, Data);
    }
}