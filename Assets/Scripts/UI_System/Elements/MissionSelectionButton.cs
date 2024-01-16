using TMPro;
using UnityEngine;

namespace UI_System.Elements
{
    public class MissionSelectionButton : MonoBehaviour
    {
        [SerializeField] [Range(0,20)] private int levelIndex;
        [SerializeField] private GameObject earnedStar1;
        [SerializeField] private GameObject earnedStar2;
        [SerializeField] private GameObject earnedStar3;
        [SerializeField] private TextMeshProUGUI textMeshPro;

        private void Awake()
        {
            textMeshPro.text = $"{levelIndex + 1}";
        }

        private void Start()
        {
            UpdateEarnedStars(PlayerGlobalData.MissionsStarsData[levelIndex]);
        }
        
        private void UpdateEarnedStars(int starsCount)
        {
            GameObject[] stars = new[] { earnedStar1, earnedStar2, earnedStar3 };

            for (int i = 0; i < stars.Length && i < starsCount; i++)
                stars[i].SetActive(true);
        }
    }
}
