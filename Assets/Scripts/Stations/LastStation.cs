using TMPro;
using UnityEngine;

public class LastStation : MonoBehaviour
{
    public Station station;
    public PlayerTravel playerTravel;
    public float speedMultiplier;
    public GameObject endPanel;
    public TextMeshProUGUI statisticsText;
    public TextMeshProUGUI achievementsText;

    private void OnEnable()
    {
        PlayerTravel.OnDestinationReached += OnDestinationReached;
    }

    public void GoToLastStation()
    {
        playerTravel.travelSpeed *= speedMultiplier;
        playerTravel.speedToAdhereToLookAhead *= speedMultiplier;
        TravelManager.Instance.Travel(TravelDirection.Front);
    }

    private void OnDestinationReached(Station _station)
    {
        if (_station == station)
        {
            endPanel.SetActive(true);
            statisticsText.text = ScoreManager.Instance.GetFinalScore();
            achievementsText.text = ScoreManager.Instance.GetAchievements();
        }
    }
}
