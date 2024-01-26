namespace Saves
{
    public abstract class SaveBase<TData>
    {
        public abstract void SetData(TData data);
    }
}