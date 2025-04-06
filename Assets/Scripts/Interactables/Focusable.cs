using System;
using UnityEngine;

public class Focusable : Clickable
{
    public Transform pov;

    public static event Action<Transform> OnGainFocus;

    protected override void Interact()
    {
        OnGainFocus?.Invoke(pov);
    }

}
