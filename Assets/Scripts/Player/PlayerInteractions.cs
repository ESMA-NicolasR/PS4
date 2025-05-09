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
        if(currentStation!=null)
            currentStation.EnableAllInteractables();
    }

    private void OnGainFocus(Focusable focus)
    {
        currentStation.DisableAllInteractables();
        currentFocus = focus;
    }

    private void OnLoseFocus()
    {
        currentStation.EnableAllInteractables();
        currentFocus.LoseFocus();
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
