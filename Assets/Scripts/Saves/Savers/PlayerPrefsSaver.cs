using UnityEngine;

namespace Saves.Savers
{
    public class PlayerPrefsSaver : ISaver
    {
        public void Save<TData, TSave>(string saveKey, TData data)
            where TData : DataBase<TSave>
            where TSave : SaveBase<TData>, new()
        {
            var save = new TSave();
            save.SetData(data);
            
            var saveString = JsonUtility.ToJson(save);
            PlayerPrefs.SetString(saveKey, saveString);
            PlayerPrefs.Save();
        }

        public TData Load<TData, TSave>(string saveKey)
            where TData : DataBase<TSave>, new()
            where TSave : SaveBase<TData>
        {
            if (!PlayerPrefs.HasKey(saveKey)) return new TData();
            
            var saveString = PlayerPrefs.GetString(saveKey);
            var save = JsonUtility.FromJson<TSave>(saveString);
            var data = new TData();
            data.SetData(save);
            return data;
        }
    }
}