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
            Debug.Log("PlayerGlobalDataInitializer");
            PlayerGlobalData.LoadData();
            OnParentInit?.Invoke();
        }
    }
}
