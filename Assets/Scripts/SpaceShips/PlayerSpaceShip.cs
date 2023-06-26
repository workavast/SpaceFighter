using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSpaceShip : SpaceShipBase
{
    protected override void OnUpdate()
    {
        base.OnUpdate();

        Move(Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position);
        
        fireRate.ChangeCurrentValue(Time.deltaTime);
        if (fireRate.IsFull)
        {
            Shoot();
            fireRate.SetCurrentValue(0);
        }
    }
}