﻿using System;

namespace Initializers
{
    public abstract class InitializerBase
    {
        protected readonly Action OnParentInit;
        public event Action OnInit;

        private int _inits;
        private readonly InitializerBase[] _initializers;
        
        protected InitializerBase(InitializerBase[] initializers = null)
        {
            if (initializers is null || initializers.Length <= 0)
            {
                _inits = 0;
                OnParentInit += UpdateInits;
            }
            else
            {
                _initializers = initializers;
                _inits = _initializers.Length;
            
                foreach (var initializer in _initializers)
                    initializer.OnInit += UpdateInits;

                OnParentInit += InitChildren;
            }
        }
        
        public abstract void Init();
        
        private void InitChildren()
        {
            foreach (var initializer in _initializers)
                initializer.Init();
        }

        private void UpdateInits()
        {
            _inits -= 1;
            
            if(_inits <= 0)
                OnInit?.Invoke();
        }
    }
}