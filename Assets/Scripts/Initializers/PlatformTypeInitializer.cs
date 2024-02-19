using Saves;
using UnityEngine;

namespace Initializers
{
    public class PlatformTypeInitializer : InitializerBase
    {
        private PlatformType _platformType = PlatformType.Mobile;
        
        public PlatformTypeInitializer(InitializerBase[] initializers = null) 
            : base(initializers) { }
        
        public override void Init()
        {
            Debug.Log("ControlInitializer");
            PlayerGlobalData.SetPlatformType(_platformType);
            OnParentInit?.Invoke();
        }
    }
}