using Saves.Savers;

namespace Saves.Spaceship
{
    public class SpaceshipSettings : SettingsBase<SpaceshipData, SpaceshipSave>
    {
        protected override string SaveKey => "SpaceshipSettings";
        
        public int SpaceshipLevel => Data.CurrentSpaceshipLevel;

        public SpaceshipSettings(ISaver saver) : base(saver) { }
        
        public void LevelUpSpaceship()
        {
            Data.IncreaseSpaceshipLevel();
            Save();
        }
    }
}