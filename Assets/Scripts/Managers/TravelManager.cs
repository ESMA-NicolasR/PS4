using System;
using UnityEngine;

public class TravelManager : MonoBehaviour
{
    public static TravelManager Instance;
    public PlayerTravel playerTravel;
    public TravelButton travelLeft, travelRight, travelBack;

    private void OnEnable()
    {
        PlayerTravel.OnDestinationReached += OnDestinationReached;
        PlayerTravel.OnTravelStart += OnTravelStart;
    }

    private void OnDisable()
    {
        PlayerTravel.OnDestinationReached -= OnDestinationReached;
        PlayerTravel.OnTravelStart -= OnTravelStart;
    }

    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        OnDestinationReached(playerTravel.currentStation);
    }

    public void Travel(TravelDirection travelDirection)
    {
        playerTravel.MoveDirection(travelDirection);
    }

    private void OnDestinationReached(Station station)
    {
        // Check left path
        var leftPath = station.GetPath(TravelDirection.Left);
        if (leftPath != null)
        {
            travelLeft.gameObject.SetActive(true);
            travelLeft.SetDestinationName(leftPath.endStation.stationName.ToString());
        }
        else
        {
            travelLeft.gameObject.SetActive(false);
        }
        // Check right path
        var rightPath = station.GetPath(TravelDirection.Right);
        if (rightPath != null)
        {
            travelRight.gameObject.SetActive(true);
            travelRight.SetDestinationName(rightPath.endStation.stationName.ToString());
        }
        else
        {
            travelRight.gameObject.SetActive(false);
        }
        // Check back path
        var backPath = station.GetPath(TravelDirection.Back);
        if (backPath != null)
        {
            travelBack.gameObject.SetActive(true);
            travelBack.SetDestinationName(backPath.endStation.stationName.ToString());
        }
        else
        {
            travelBack.gameObject.SetActive(false);
        }
    }

    private void OnTravelStart()
    {
        travelLeft.gameObject.SetActive(false);
        travelRight.gameObject.SetActive(false);
        travelBack.gameObject.SetActive(false);
    }
}
