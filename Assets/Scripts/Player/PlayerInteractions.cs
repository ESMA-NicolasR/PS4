using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public Focusable currentFocus;
    public Station currentStation;

    private void OnEnable()
    {
        Focusable.OnGainFocus += OnGainFocus;
        PlayerFocus.OnLoseFocus += OnLoseFocus;
        PlayerTravel.OnTravelStart += OnTravelStart;
        PlayerTravel.OnDestinationReached += OnDestinationReached;
    }

    void Start()
    {
        currentStation.EnableAllInteractables();
    }

    private void OnGainFocus(Focusable focus)
    {
        currentStation.DisableAllInteractables();
        currentFocus = focus;
        focus.EnableInteractables();
    }

    private void OnLoseFocus()
    {
        currentStation.EnableAllInteractables();
        currentFocus = null;
    }

    private void OnTravelStart()
    {
        currentStation.DisableAllInteractables();
        currentStation = null;
    }

    private void OnDestinationReached(Station station)
    {
        currentStation = station;
        currentStation.EnableAllInteractables();
    }
}
