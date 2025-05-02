using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Focusable : Clickable
{
    public Transform pov;
    private List<Clickable> _clickables;
    public static event Action<Focusable> OnGainFocus;
    public bool isFocused;

    protected override void Awake()
    {
        base.Awake();
        _clickables = GetComponentsInChildren<Clickable>().ToList();
        _clickables.Remove(this);
        foreach (var clickable in _clickables)
        {
            clickable.focusParent = this;
        }
    }

    protected override void Interact()
    {
        if (isFocused) return;
        
        GainFocus();
    }

    public void GainFocus()
    {
        OnGainFocus?.Invoke(this);
        EnableInteractables();
        Disable();
        isFocused = true;
    }

    public override void EnableHighlight()
    {
        base.EnableHighlight();
        foreach (var clickable in _clickables)
        {
            clickable.EnableHighlight();
        }
    }

    public override void DisableHighlight()
    {
        base.DisableHighlight();
        foreach (var clickable in _clickables)
        {
            clickable.DisableHighlight();
        }
    }

    public void EnableInteractables()
    {
        foreach (var clickable in _clickables)
        {
            clickable.Enable();
        }
    }

    public void LoseFocus()
    {
        Enable();
        isFocused = false;
    }
    
    
    public void DisableInteractables()
    {
        foreach (var clickable in _clickables)
        {
            clickable.Disable();
        }
    }
}
