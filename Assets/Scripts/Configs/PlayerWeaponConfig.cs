using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerWeaponsConfig", menuName = "SO/PlayerWeaponsConfig")]
    public class PlayerWeaponConfig : ScriptableObject
    {
        [Serializable]
        private struct WeaponDataCell
        {
            [field: SerializeField] public GameObject WeaponPrefab { get; private set; }
            [field: SerializeField] public List<WeaponLevel> WeaponLevels { get; private set; }
        }

        [SerializeField] private DictionaryInspector<PlayerWeaponsEnum, WeaponDataCell> playerWeaponsData;
    
        public IReadOnlyDictionary<PlayerWeaponsEnum, GameObject> WeaponsPrefabsData =>
            playerWeaponsData.ToDictionary(x => x.Key, x => x.Value.WeaponPrefab);
        public IReadOnlyDictionary<PlayerWeaponsEnum, List<WeaponLevel>> WeaponsLevelsData =>
            playerWeaponsData.ToDictionary(x => x.Key, x => x.Value.WeaponLevels);
        public IReadOnlyDictionary<PlayerWeaponsEnum, List<int>> WeaponPricesData =>
            playerWeaponsData.ToDictionary(x => x.Key, x => x.Value.WeaponLevels.Select(y => y.Price).ToList());
    }
}
