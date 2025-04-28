using UnityEngine;

public class TravelManager : MonoBehaviour
{
    public static TravelManager Instance;
    public PlayerTravel playerTravel;
    // TODO use serializable dicts ?
    public GameObject travelLeft, travelRight, travelBack;

    void Start()
    {
        // Singleton
        Instance = this;
        PlayerTravel.OnDestinationReached += OnDestinationReached;
        PlayerTravel.OnTravelStart += OnTravelStart;
        OnDestinationReached(playerTravel.currentStation);
    }

    public void Travel(TravelDirection travelDirection)
    {
        playerTravel.MoveDirection(travelDirection);
    }

    private void OnDestinationReached(Station station)
    {
        travelLeft.SetActive(station.GetPath(TravelDirection.Left) != null);
        travelRight.SetActive(station.GetPath(TravelDirection.Right) != null);
        travelBack.SetActive(station.GetPath(TravelDirection.Back) != null);
    }

    private void OnTravelStart()
    {
        travelLeft.SetActive(false);
        travelRight.SetActive(false);
        travelBack.SetActive(false);
    }
}
