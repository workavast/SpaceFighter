using System;
using System.Collections.Generic;
using System.Linq;

namespace Saves.Missions
{
    [Serializable]
    public sealed class MissionsSettingsSave
    {
        public List<int> MissionsStarsData;

        public MissionsSettingsSave(MissionsSettings settings) 
            => MissionsStarsData = settings.MissionsStarsData.ToList();
    }
}