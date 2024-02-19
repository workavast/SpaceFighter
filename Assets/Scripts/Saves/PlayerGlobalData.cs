using Initializers;
using Saves.Coins;
using Saves.Localization;
using Saves.Missions;
using Saves.Savers;
using Saves.Spaceship;
using Saves.Volume;
using Saves.Weapons;

namespace Saves
{
    public static class PlayerGlobalData
    {
        public static PlatformType PlatformType { get; private set; }
        public static VolumeSettings VolumeSettings { get; } = new(new PlayerPrefsSaver());
        public static MissionsSettings MissionsSettings { get; } = new(new PlayerPrefsSaver());
        public static CoinsSettings CoinsSettings { get; } = new(new PlayerPrefsSaver());
        public static SpaceshipSettings SpaceshipSettings { get; } = new(new PlayerPrefsSaver());
        public static WeaponsSettings WeaponsSettings { get; } = new(new PlayerPrefsSaver());
        public static LocalizationSettings LocalizationSettings { get; } = new(new PlayerPrefsSaver());
        
        public static void SetPlatformType(PlatformType newPlatformType) => PlatformType = newPlatformType;
        
        public static void LoadData()
        {
            VolumeSettings.Load();
            MissionsSettings.Load();
            CoinsSettings.Load();
            SpaceshipSettings.Load();
            WeaponsSettings.Load();
            LocalizationSettings.Load();
        }
    }
}
