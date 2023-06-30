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
    public static PlayerSpaceShip Instance { get; private set; }

    protected override void OnAwake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }
        
        base.OnAwake();
        Instance = this;
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
        
        Move();
        
        fireRate.ChangeCurrentValue(Time.deltaTime);
        if (fireRate.IsFull)
        {
            Shoot();
            fireRate.SetCurrentValue(0);
        }
    }
    
    protected void Move()
    {
        Vector3 targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition) - _camera.transform.position;
        Vector3 playAreaPivotPosition = _playAreaLeftDownPivot.position;

        float x = Mathf.Clamp(targetPosition.x, playAreaPivotPosition.x, -playAreaPivotPosition.x);
        float y = Mathf.Clamp(targetPosition.y, playAreaPivotPosition.y, -playAreaPivotPosition.y);

        targetPosition = new Vector3(x, y, targetPosition.z);
        
        float finalSpeed = Mathf.Clamp(Vector3.Distance(transform.position, targetPosition), 0, moveSpeed * Time.deltaTime);

        transform.Translate((targetPosition - transform.position).normalized * finalSpeed);
    }
}