using System;

namespace Settings
{
    public interface ISettings
    {
        public event Action OnChange;
    }
}