using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MoneyStarPrefabConfig", menuName = "SO/Factories/MoneyStarPrefabConfig")]
    public class MoneyStarPrefabConfig : ScriptableObject
    {
        [SerializeField] private GameObject data;
        public GameObject Data => data;
    }
}
