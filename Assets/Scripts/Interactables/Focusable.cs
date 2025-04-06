using System;
using UnityEngine;

public class Focusable : MonoBehaviour
{
    public Transform pov;

    public static event Action<Transform> OnGainFocus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        OnGainFocus?.Invoke(pov);
    }

}
