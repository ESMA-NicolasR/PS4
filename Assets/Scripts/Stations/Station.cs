using System;
using UnityEngine;

public class Station : MonoBehaviour
{
    public TravelPath leftPath, rightPath, backPath, frontPath;
    public StationName stationName;
    private Clickable[] _clickables;
    private Focusable[] _focusables;
    
    private void Awake()
    {
        _clickables = GetComponentsInChildren<Clickable>();
        _focusables = GetComponentsInChildren<Focusable>();
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
            case TravelDirection.Front:
                return frontPath;
            default:
                return null;
        }
    }

    public void DisableAllInteractables()
    {
        foreach (var clickable in _clickables)
        {
            clickable.Disable();
        }
    }

    public void EnableAllInteractables()
    {
        foreach (var clickable in _clickables)
        {
            clickable.Enable();
        }
        // Subcomponents of focusable should not be enabled
        foreach (var focusable in _focusables)
        {
            focusable.LoseFocus();            
        }
    }

    private void OnValidate()
    {
        if (stationName == StationName.None)
        {
            throw new ArgumentException($"Station {gameObject.name} needs a valid StationName", "StationName");
        }
    }
}
