using System;

namespace Settings.Spaceship
{
    [Serializable]
    public sealed class SpaceshipSettingsSave
    {
        public int CurrentSpaceshipLevel = 1;

        public SpaceshipSettingsSave()
        {
            CurrentSpaceshipLevel = 1;
        }
        
        public SpaceshipSettingsSave(SpaceshipSettings settings)
        {
            CurrentSpaceshipLevel = settings.SpaceshipLevel;
        }
    }
}