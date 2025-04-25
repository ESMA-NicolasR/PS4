using UnityEngine;

public class TravelButton : MonoBehaviour
{
    public TravelDirection travelDirection;
    public GameObject panel;

    private void Start()
    {
        PlayerFocus.OnLoseFocus += Show;
        Focusable.OnGainFocus += Hide;
    }

    public void Travel()
    {
        TravelManager.Instance.Travel(travelDirection);
    }

    private void Hide(Transform _)
    {
        panel.SetActive(false);
    }
    
    private void Show()
    {
        panel.SetActive(true);
    }
}
