using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Focusable : Clickable
{
    public Transform pov;
    private List<Clickable> _clickables;
    public static event Action<Focusable> OnGainFocus;
    protected override CursorType cursorType => CursorType.Eye;
    
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
        GainFocus();
        clickableSound.PlayMySound();
    }

    public virtual void GainFocus()
    {
        OnGainFocus?.Invoke(this);
        EnableInteractables();
        CursorManager.Instance.ChangeCursor(CursorType.Open);
        Disable();
    }

    public override void EnableHighlight()
    {
        foreach (var clickable in _clickables)
        {
            clickable.EnableHighlight();
        }
        base.EnableHighlight();
    }

    public override void DisableHighlight()
    {
        foreach (var clickable in _clickables)
        {
            clickable.DisableHighlight();
        }
        base.DisableHighlight();
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
