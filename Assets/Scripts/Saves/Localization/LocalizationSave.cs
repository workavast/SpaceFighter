namespace Saves.Localization
{
    public class LocalizationSave : SaveBase<LocalizationData>
    {
        public int LocalizationId;
        
        public override void SetData(LocalizationData data) => LocalizationId = data.LocalizationId;
    }
}