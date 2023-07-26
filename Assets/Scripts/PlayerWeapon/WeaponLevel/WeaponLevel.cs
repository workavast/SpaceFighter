using System;
using UnityEngine;

[Serializable]
public struct WeaponLevel
{
    [field: SerializeField] public float WeaponDamage { get; private set; }
    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public uint ShootsCountScale { get; private set; }
}