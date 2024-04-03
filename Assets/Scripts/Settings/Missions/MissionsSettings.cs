using System;
using System.Collections.Generic;
using UnityEngine;

namespace Settings.Missions
{
    public sealed class MissionsSettings : ISettings
    {
        private List<int> _missionsStarsData;
        
        public IReadOnlyList<int> MissionsStarsData => _missionsStarsData;
        
        public event Action OnChange;
        
        public MissionsSettings(int missionsCount = 21)
        {
            _missionsStarsData = new List<int>();
                    
            for (int i = 0; i < missionsCount; i++)
                _missionsStarsData.Add(0);
        }
            
        public void SetData(MissionsSettingsSave missionsSettingsSave)
        {
            _missionsStarsData = missionsSettingsSave.MissionsStarsData;
        }
    
        public void ChangeMissionData(int missionIndex, int newStarsCount)
        {
            if (missionIndex < 0 && missionIndex > _missionsStarsData.Count) throw new Exception("Unsigned level num");
            if (newStarsCount < 0)
            {
                newStarsCount = 0;
                Debug.LogError("Unsigned level num");
            }
            if (newStarsCount > 3)
            {
                newStarsCount = 3;
                Debug.LogError("Unsigned level num");
            }
                    
            var oldStarsCount = _missionsStarsData[missionIndex];
            if(newStarsCount <= oldStarsCount) return;
    
            _missionsStarsData[missionIndex] = newStarsCount;
            OnChange?.Invoke();
        }
    }
}