using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace Localization
{
    [Serializable]
    public class LocalizedPair : Disposable
    {
        [SerializeField] protected LocalizedString localizedString;
        public string Str { get; protected set; }

        public event Action<string> OnStringChange; 
            
        public virtual void Init()
        {
            localizedString.StringChanged += UpdateString;
            Str = localizedString.GetLocalizedString();
        }

        private void UpdateString(string newStr)
        {
            Str = newStr;
            OnStringChange?.Invoke(Str);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            localizedString.StringChanged -= UpdateString;
        }
    }

    [Serializable]
    public class LocalizedPair<T> : LocalizedPair
        where T: struct
    {
        private T _value;

        public override void Init()
        {
            base.Init();
                
            T zero = default;
            localizedString.Arguments = new List<object>(){zero};
        }
            
        public void SetValue(T value)
        {
            localizedString.Arguments[0] = value;
            localizedString.RefreshString();
        }
    }
}