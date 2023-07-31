using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoneyStarPrefabConfig", menuName = "SO/Factories/MoneyStarPrefabConfig")]
public class MoneyStarPrefabConfig : ScriptableObject
{
    [SerializeField] private GameObject data;
    public GameObject Data => data;
}
