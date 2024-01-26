using System.Collections.Generic;
using Saves.Savers;

namespace Saves.Missions
{
    public sealed class MissionsSettings : SettingsBase<MissionsData, MissionsSave>
    {
        protected override string SaveKey => "MissionsSettings";
    
        public IReadOnlyList<int> MissionsStarsData => Data.MissionsStarsData;
        
        public MissionsSettings(ISaver saver) : base(saver) { }
        
        public void ChangeMissionData(int missionIndex, int starCount)
        {
            Data.ChangeMissionData(missionIndex, starCount);
            Save();
        }
    }
}