using Settings;
using UnityEngine;
using YG;

namespace Initializers
{
    public class PlatformTypeInitializer : InitializerBase
    {
        public PlatformTypeInitializer(InitializerBase[] initializers = null) 
            : base(initializers) { }
        
        public override void Init()
        {
            Debug.Log("-||- PlatformTypeInitializer");

            var device = YandexGame.EnvironmentData.deviceType;
            PlatformType platformType;
            switch (device)
            {
                case "desktop":
                    platformType = PlatformType.Desktop;
                    break;
                default: 
                    platformType = PlatformType.Mobile;
                    break;
            }
                
            PlayerGlobalData.Instance.SetPlatformType(platformType);
            OnParentInit?.Invoke();
        }
    }
}