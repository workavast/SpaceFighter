using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerProjectileBase : ProjectileBase<PlayerProjectilesEnum, PlayerProjectileBase>
{
    public void SetData(float newDamage)
    {
        damage = newDamage;
    }
}
