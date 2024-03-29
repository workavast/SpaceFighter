using Saves;
using UnityEngine;

namespace Initializers
{
    public class PlayerGlobalDataInitializer : InitializerBase
    {
        public PlayerGlobalDataInitializer(InitializerBase[] initializers = null) 
            : base(initializers) { }

        public override void Init()
        {
            PlayerGlobalData.Instance.OnInit += InvokeParenInit;
            PlayerGlobalData.Instance.Initialize();
        }
        
        private void InvokeParenInit()
        {
            Debug.Log("-||- PlayerGlobalDataInitializer");
            OnParentInit?.Invoke();
        }
    }
}
