using System;
using System.Collections.Generic;
using System.Linq;

namespace Settings.Missions
{
    [Serializable]
    public sealed class MissionsSettingsSave
    {
        public List<int> MissionsStarsData;

        public MissionsSettingsSave()
        {
            MissionsStarsData = new List<int>();
                    
            for (int i = 0; i < 21; i++)
                MissionsStarsData.Add(0);
        }
        
        public MissionsSettingsSave(MissionsSettings settings) 
            => MissionsStarsData = settings.MissionsStarsData.ToList();
    }
}