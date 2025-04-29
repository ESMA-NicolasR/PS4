using System;
using UnityEngine;

public class Station : MonoBehaviour
{
    public TravelPath leftPath, rightPath, backPath;

    private void OnEnable()
    {
        PlayerTravel.OnTravelStart += DisableAllInteractables;
        PlayerTravel.OnDestinationReached += OnDestinationReached;
    }

    private void OnDisable()
    {
        PlayerTravel.OnTravelStart -= DisableAllInteractables;
    }

    public TravelPath GetPath(TravelDirection direction)
    {
        switch (direction)
        {
            case TravelDirection.Left:
                return leftPath;
            case TravelDirection.Right:
                return rightPath;
            case TravelDirection.Back:
                return backPath;
            default:
                return null;
        }
    }

    public void DisableAllInteractables()
    {
        foreach (var clickable in transform.GetComponentsInChildren<Clickable>())
        {
            clickable.Disable();
        }
    }

    public void EnableAllInteractables()
    {
        foreach (var clickable in transform.GetComponentsInChildren<Clickable>())
        {
            clickable.Enable();
        }
    }
    private void OnDestinationReached(Station station)
    {
        if (station == this)
        {
            EnableAllInteractables();
        }
    }
}
