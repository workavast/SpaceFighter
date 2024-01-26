using Saves.Savers;

namespace Saves.Localization
{
    public class LocalizationSettings : SettingsBase<LocalizationData, LocalizationSave>
    {
        protected override string SaveKey => "LocalizationSettings";

        public int LocalizationId => Data.LocalizationId;
        
        public LocalizationSettings(ISaver saver) : base(saver) { }

        public void ChangeLocalization(int newLocalizationId)
        {
            Data.ChangeLocalization(newLocalizationId);
            Save();
        }
    }
}