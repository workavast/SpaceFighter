using UnityEngine;

namespace Saves.Savers
{
    public class PlayerPrefsSaver : ISaver
    {
        public void Save(string saveKey, PlayerGlobalDataSave save)
        {
            var saveString = JsonUtility.ToJson(save);
            PlayerPrefs.SetString(saveKey, saveString);
            PlayerPrefs.Save();
        }

        public bool TryLoad(string saveKey, out PlayerGlobalDataSave save)
        {
            save = default;
            if (!PlayerPrefs.HasKey(saveKey)) 
                return false;
            
            var saveString = PlayerPrefs.GetString(saveKey);
            save = JsonUtility.FromJson<PlayerGlobalDataSave>(saveString);
            return true;
        }
    }
}