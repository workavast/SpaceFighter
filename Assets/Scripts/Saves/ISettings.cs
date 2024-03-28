using System;

namespace Saves
{
    public interface ISettings
    {
        public event Action OnChange;
    }
}