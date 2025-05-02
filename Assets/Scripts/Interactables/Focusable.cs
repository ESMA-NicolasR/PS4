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
        foreach (var clickable in _clickables)
        {
            clickable.OnClick.AddListener(Interact);
        }
    }

    protected override void Interact()
    {
        OnGainFocus?.Invoke(this);
        EnableInteractables();
        Disable();
    }

    protected override void OnMouseEnter()
    {
        if (!_canBeUsed) return;
        EnableHighlight();
        foreach (var clickable in _clickables)
        {
            clickable.EnableHighlight();
        }
    }
    
    protected override void OnMouseExit()
    {
        if (!_canBeUsed) return;
        DisableHighlight();
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
    }
    
    public void DisableInteractables()
    {
        foreach (var clickable in _clickables)
        {
            clickable.Disable();
        }
    }
    
    

}
