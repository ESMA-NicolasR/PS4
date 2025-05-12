using System;
using UnityEngine;

public class MeteorCaseBehavior : MonoBehaviour
{
    public Minigame_Network minigameNetwork;

    private void OnEnable()
    {
        minigameNetwork = GameObject.Find("Network").GetComponent<Minigame_Network>();
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            minigameNetwork.Out();
        }
    }
}
