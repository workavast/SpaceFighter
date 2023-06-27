using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSpaceShip : SpaceShipBase
{
    private Transform _playAreaLeftDownPivot;
    private Camera _camera;

    protected override void OnAwake()
    {
        base.OnAwake();
        _camera = Camera.main;
    }

    protected override void OnStart()
    {
        base.OnStart();
        _playAreaLeftDownPivot = PlayArea.Instance.LeftDownPivot;
    }
    
    protected override void OnUpdate()
    {
        base.OnUpdate();

        Vector3 targetPoint = _camera.ScreenToWorldPoint(Input.mousePosition) - _camera.transform.position;
        Vector3 playAreaPivotPosition = _playAreaLeftDownPivot.position;

        float x = Mathf.Clamp(targetPoint.x, playAreaPivotPosition.x, -playAreaPivotPosition.x);
        float y = Mathf.Clamp(targetPoint.y, playAreaPivotPosition.y, -playAreaPivotPosition.y);

        targetPoint = new Vector3(x, y, targetPoint.z);
            
        Move(targetPoint);
        
        fireRate.ChangeCurrentValue(Time.deltaTime);
        if (fireRate.IsFull)
        {
            Shoot();
            fireRate.SetCurrentValue(0);
        }
    }
}