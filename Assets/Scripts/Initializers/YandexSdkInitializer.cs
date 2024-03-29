using UnityEngine;
using YG;

namespace Initializers
{
    public class YandexSdkInitializer : InitializerBase
    {
        public YandexSdkInitializer(InitializerBase[] initializers = null) 
            : base(initializers) { }

        public override void Init() => WaitLoad();

        private void WaitLoad()
        {
            while (!YandexGame.SDKEnabled)
                Debug.Log("Await");
            
            Debug.Log("-||- YandexSdkInitializer");
            OnParentInit?.Invoke();
        }
    }
}