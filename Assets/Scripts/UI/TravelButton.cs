using UnityEngine;

public class TravelButton : MonoBehaviour
{
    public TravelDirection travelDirection;
    public GameObject panel;

    private void OnEnable()
    {
        PlayerFocus.OnLoseFocus += Show;
        Focusable.OnGainFocus += Hide;
    }
    
    private void OnDisable()
    {
        PlayerFocus.OnLoseFocus += Show;
        Focusable.OnGainFocus += Hide;
    }


    public void Travel()
    {
        TravelManager.Instance.Travel(travelDirection);
    }

    private void Hide(Focusable _)
    {
        panel.SetActive(false);
    }
    
    private void Show()
    {
        panel.SetActive(true);
    }
}
