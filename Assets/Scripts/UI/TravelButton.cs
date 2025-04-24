using UnityEngine;

public class TravelButton : MonoBehaviour
{
    public TravelDirection travelDirection;

    public void Travel()
    {
        TravelManager.Instance.Travel(travelDirection);
    }
}
