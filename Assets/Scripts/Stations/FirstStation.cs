using System.Collections;
using UnityEngine;

public class FirstStation : MonoBehaviour
{
    private TravelManager travelManager;


    private IEnumerator Start()
    {
        FindFirstObjectByType<CursorMoveCamera>().canMove = false;
        yield return new WaitForSeconds(1.5f);
        travelManager = FindFirstObjectByType<TravelManager>();
        travelManager.Travel(TravelDirection.Front);
    }
}
