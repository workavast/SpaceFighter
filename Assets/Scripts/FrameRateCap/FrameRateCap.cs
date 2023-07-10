using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateCap : MonoBehaviour
{
    private static FrameRateCap _instance;
    
    private void Awake()
    {
        if (_instance)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this);
    }
}
