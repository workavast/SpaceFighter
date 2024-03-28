using System;

namespace Saves.Spaceship
{
    [Serializable]
    public sealed class SpaceshipSettingsSave
    {
        public int CurrentSpaceshipLevel;

        public SpaceshipSettingsSave(SpaceshipSettings settings)
        {
            CurrentSpaceshipLevel = settings.SpaceshipLevel;
        }
    }
}