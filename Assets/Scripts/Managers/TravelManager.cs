using System;
using UnityEngine;

public class TravelManager : MonoBehaviour
{
    public static TravelManager Instance;
    public PlayerTravel playerTravel;
    // TODO use serializable dicts ?
    public GameObject travelLeft, travelRight, travelBack;

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
