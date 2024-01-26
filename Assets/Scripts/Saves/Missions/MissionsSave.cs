using System.Collections.Generic;
using System.Linq;

namespace Saves.Missions
{
    public class MissionsSave : SaveBase<MissionsData>
    {
        public List<int> MissionsStarsData;
        
        public override void SetData(MissionsData missionsData)
        {
            MissionsStarsData = missionsData.MissionsStarsData.ToList();
        }
    }
}