using System.Collections.Generic;
using Projectiles.Player;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerProjectilesConfig", menuName = "SO/PlayerProjectilesConfig")]
    public class PlayerProjectilesConfig : ScriptableObject
    {
        [SerializeField] private DictionaryInspector<PlayerProjectilesEnum, GameObject> data;
        public IReadOnlyDictionary<PlayerProjectilesEnum, GameObject> Data => data;
    }
}