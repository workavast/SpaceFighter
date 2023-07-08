using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraAspect : MonoBehaviour
{
    [SerializeField] private Camera someCamera;
    [SerializeField] private float aspect;

    private void Update()
    {
        float width = Screen.height * aspect;
        float w = width / Screen.width;
        float x = (1 - w) / 2f;
        someCamera.rect = new Rect(x, 0,w, 1);
    }
}
