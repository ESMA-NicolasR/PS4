using System.Collections;
using UnityEngine;

public class FirstStation : MonoBehaviour
{
    private TravelManager travelManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private IEnumerator Start()
    {
        travelManager = FindFirstObjectByType<TravelManager>();
        yield return new WaitForSeconds(0.5f);
        travelManager.Travel(TravelDirection.Back);
    }
}
