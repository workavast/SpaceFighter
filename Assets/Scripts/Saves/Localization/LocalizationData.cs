namespace Saves.Localization
{
    public class LocalizationData : DataBase<LocalizationSave>
    {
        public int LocalizationId { get; private set; }
        
        public override void SetData(LocalizationSave save) => LocalizationId = save.LocalizationId;

        public void ChangeLocalization(int newLocalizationId) => LocalizationId = newLocalizationId;
    }
}