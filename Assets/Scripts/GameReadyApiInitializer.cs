using UnityEngine;
using YG;

public class GameReadyApiInitializer : MonoBehaviour
{
    private void Awake()
    {
        YandexGame.GameReadyAPI();
    }
}
