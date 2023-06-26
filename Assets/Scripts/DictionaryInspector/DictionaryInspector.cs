using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DictionaryInspector<TKey, TValue> : Dictionary<TKey,TValue>, ISerializationCallbackReceiver
{
    [Serializable]
    private struct DictionaryCell
    {
        public TKey key;
        public TValue value;

        public DictionaryCell(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }

    [SerializeField] private List<DictionaryCell> dictionaryCells = new List<DictionaryCell>();

    void ISerializationCallbackReceiver.OnBeforeSerialize() { }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        Clear();
        foreach (var cell in dictionaryCells)
            this[cell.key] = cell.value;
    }
}
