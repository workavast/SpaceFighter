using TMPro;
using UnityEngine;

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

    private void OnEnable()
    {
        UpdateEarnedStars(PlayerGlobalData.MissionsData[levelIndex]);
    }
    
    private void UpdateEarnedStars(PlayerGlobalData.MissionCell missionCell)
    {
        earnedStar1.SetActive(missionCell.star_1);
        earnedStar2.SetActive(missionCell.star_2);
        earnedStar3.SetActive(missionCell.star_3);
    }
}
