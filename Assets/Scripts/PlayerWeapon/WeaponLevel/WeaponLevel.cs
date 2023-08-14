using System;
using UnityEngine;

[Serializable]
public struct WeaponLevel
{
    [field: SerializeField] public float WeaponDamage { get; private set; }
    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public int ShootsCountScale { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
}