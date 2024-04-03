using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = nameof(CoinConfig), menuName = "SO/" + nameof(CoinConfig))]
    public class CoinConfig : ScriptableObject
    {
        [field: SerializeField] [field: Range(0, 10)] 
        public float MoveSpeed { get; private set; }
    }
}