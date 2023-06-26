using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpaceShip : SpaceShipBase
{
    [SerializeField] private AnimationCurve way;
    
    protected override void OnUpdate()
    {
        base.OnUpdate();
    }
}