using System.Collections.Generic;
using Projectiles.Player;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerProjectilesConfig", menuName = "SO/PlayerProjectilesConfig")]
    public class PlayerProjectilesConfig : ScriptableObject
    {
        [SerializeField] private DictionaryInspector<PlayerProjectileType, GameObject> data;
        public IReadOnlyDictionary<PlayerProjectileType, GameObject> Data => data;
    }
}