using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSpaceshipLevelsConfig", menuName = "SO/PlayerSpaceshipLevelsConfig")]
public class PlayerSpaceshipLevelsConfig : ScriptableObject
{
    [Serializable]
    private class SpaceshipLevelData
    {
        [field: SerializeField] public int HealthPoints { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
    }
    
    [SerializeField] private List<SpaceshipLevelData> data;

    public int LevelsCount => data.Count;
    public IReadOnlyList<int> LevelsHealthPoints => data.Select(x => x.HealthPoints).ToList();
    public IReadOnlyList<int> LevelsPrices => data.Select(x => x.Price).ToList();
}
