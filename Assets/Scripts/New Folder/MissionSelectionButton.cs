using TMPro;
using UnityEngine;

public class MissionSelectionButton : MonoBehaviour
{
    [SerializeField] [Range(0,21)] private int levelIndex;
    [SerializeField] private GameObject earnedStar1;
    [SerializeField] private GameObject earnedStar2;
    [SerializeField] private GameObject earnedStar3;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro.text = "" + levelIndex;
    }

    private void OnEnable()
    {
        UpdateEarnedStars(PlayerGlobalData.MissionsData[levelIndex]);
    }
    
    private void UpdateEarnedStars(PlayerGlobalData.MissionCell missionCell)
    {
        if(missionCell.star_1) earnedStar1.SetActive(true);
        if(missionCell.star_2) earnedStar2.SetActive(true);
        if(missionCell.star_3) earnedStar3.SetActive(true);
    }
}
