using System;

namespace Settings.Spaceship
{
    public class SpaceshipSettings : ISettings
    {
        public int SpaceshipLevel { get; private set; }
   
        public event Action OnChange;

        public SpaceshipSettings()
        {
            SpaceshipLevel = 1;
        }
        
        public void SetData(SpaceshipSettingsSave settingsSave)
        {
            SpaceshipLevel = settingsSave.CurrentSpaceshipLevel;
        }

        public void IncreaseSpaceshipLevel()
        {
            SpaceshipLevel++;
            OnChange?.Invoke();
        }
    }
}