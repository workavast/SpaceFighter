using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CoinPrefabConfig", menuName = "SO/CoinPrefabConfig")]
    public class CoinPrefabConfig : ScriptableObject
    {
        [SerializeField] private GameObject data;
        public GameObject Data => data;
    }
}
