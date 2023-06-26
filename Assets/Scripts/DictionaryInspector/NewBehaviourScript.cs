using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private DictionaryInspector<int, int> dictionaryInspector;

    private void Awake()
    {
        if (dictionaryInspector.ContainsKey(1))
        {
            Debug.Log(dictionaryInspector[1]);
        }
        if (dictionaryInspector.ContainsKey(2))
        {
            Debug.Log("2");
        }
        if (dictionaryInspector.ContainsKey(3))
        {
            Debug.Log("3");
        }
        if (dictionaryInspector.ContainsKey(4))
        {
            Debug.Log("4");
        }
        if (dictionaryInspector.ContainsKey(51))
        {
            Debug.Log("51");
        }
        if (dictionaryInspector.ContainsKey(5536))
        {
            Debug.Log("5536");
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Application.Quit();
        }
    }
}
