namespace Saves.Savers
{
    public interface ISaver
    {
        public void Save(string saveKey, PlayerGlobalDataSave save);
        public bool TryLoad(string saveKey, out PlayerGlobalDataSave save);
    }
}