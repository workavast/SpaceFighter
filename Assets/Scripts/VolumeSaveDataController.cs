using UnityEngine;

public class VolumeSaveDataController
{
    private VolumeData _volumeData;

    public float MasterVolume => _volumeData.MasterVolume;
    public float OstVolume => _volumeData.OstVolume;
    public float EffectsVolume => _volumeData.EffectsVolume;

    public void ChangeMasterVolume(float newVolume)
    {
        _volumeData.MasterVolume = newVolume;
        SaveData();
    }
    
    public void ChangeOstVolume(float newVolume)
    {
        _volumeData.OstVolume = newVolume;
        SaveData();
    }
    
    public void ChangeEffectsVolume(float newVolume)
    {
        _volumeData.EffectsVolume = newVolume;
        SaveData();
    }
    
    public void LoadData()
    {
        if (PlayerPrefs.HasKey("VolumeSettings"))
        {
            var savingStr = PlayerPrefs.GetString("VolumeSettings");
            var savingData = JsonUtility.FromJson<VolumeSaveData>(savingStr);
            _volumeData = new VolumeData(savingData);
        }
        else
        {
            _volumeData = new VolumeData(0.5f, 0.5f, 0.5f);
        }
    }
    
    private void SaveData()
    {
        VolumeSaveData savingData = new VolumeSaveData(_volumeData);
        var str = JsonUtility.ToJson(savingData);
        PlayerPrefs.SetString("VolumeSettings", str);
        PlayerPrefs.Save();
    }
    
    private struct VolumeSaveData
    {
        public float MasterVolume;
        public float OstVolume;
        public float EffectsVolume;

        public VolumeSaveData(VolumeData volumeData)
        {
            MasterVolume = volumeData.MasterVolume;
            OstVolume = volumeData.OstVolume;
            EffectsVolume = volumeData.EffectsVolume;
        }
    }
    
    private struct VolumeData
    {
        public float MasterVolume;
        public float OstVolume;
        public float EffectsVolume;
        
        public VolumeData(VolumeSaveData volumeData)
        {
            MasterVolume = volumeData.MasterVolume;
            OstVolume = volumeData.OstVolume;
            EffectsVolume = volumeData.EffectsVolume;
        }

        public VolumeData(float masterVolume, float ostVolume, float effectsVolume)
        {
            MasterVolume = masterVolume;
            OstVolume  = ostVolume;
            EffectsVolume = effectsVolume;
        }
    }
}