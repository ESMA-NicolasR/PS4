using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Focusable : Clickable
{
    public Transform pov;
    private List<Clickable> _clickables;
    public static event Action<Focusable> OnGainFocus;

    protected override void Awake()
    {
        base.Awake();
        _clickables = GetComponentsInChildren<Clickable>().ToList();
        _clickables.Remove(this);
    }

    protected override void Interact()
    {
        OnGainFocus?.Invoke(this);
    }

    public void EnableInteractables()
    {
        foreach (var clickable in _clickables)
        {
            clickable.Enable();
        }
    }

    public virtual void LoseFocus()
    {
        DisableInteractables();
    }
    
    private void DisableInteractables()
    {
        foreach (var clickable in _clickables)
        {
            clickable.Disable();
        }
    }

}
