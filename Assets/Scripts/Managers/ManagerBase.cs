using UnityEngine;

namespace Managers
{
    public abstract class ManagerBase : MonoBehaviour
    {
        private void Awake() => OnAwake();
        
        protected virtual void OnAwake() { }
    }
}