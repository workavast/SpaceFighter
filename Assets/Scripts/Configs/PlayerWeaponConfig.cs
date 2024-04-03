using System;
using System.Collections.Generic;
using System.Linq;
using PlayerWeapon;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerWeaponsConfig", menuName = "SO/PlayerWeaponsConfig")]
    public class PlayerWeaponConfig : ScriptableObject
    {
        [SerializeField] private DictionaryInspector<PlayerWeaponType, WeaponDataCell> playerWeaponsData;
    
        public IReadOnlyDictionary<PlayerWeaponType, GameObject> WeaponsPrefabsData =>
            playerWeaponsData.ToDictionary(x => x.Key, x => x.Value.WeaponPrefab);
        public IReadOnlyDictionary<PlayerWeaponType, List<WeaponLevel>> WeaponsLevelsData =>
            playerWeaponsData.ToDictionary(x => x.Key, x => x.Value.WeaponLevels);
        public IReadOnlyDictionary<PlayerWeaponType, List<int>> WeaponPricesData =>
            playerWeaponsData.ToDictionary(x => x.Key, x => x.Value.WeaponLevels.Select(y => y.Price).ToList());
        
        [Serializable]
        private struct WeaponDataCell
        {
            [field: SerializeField] public GameObject WeaponPrefab { get; private set; }
            [field: SerializeField] public List<WeaponLevel> WeaponLevels { get; private set; }
        }
    }
}
