using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Draggable : Clickable
{
    private bool _isDragged;
    [SerializeField]
    private Transform _anchor;
    public float dragFactor;

    public static float AnalyticsTotalTimeDragging;
    
    protected override void Interact()
    {
        _isDragged = true;
        Cursor.visible = false;
        FindFirstObjectByType<CursorMoveCamera>().readInputs = false;
    }

    protected virtual void OnMouseUp()
    {
        Cursor.visible = true;
        _isDragged = false;
        FindFirstObjectByType<CursorMoveCamera>().readInputs = true;
    }

    protected override void OnMouseEnter()
    {
        // Ignore while dragging around
        if (_isDragged) return;
        base.OnMouseEnter();
    }
    
    protected override void OnMouseExit()
    {
        // Ignore while dragging around
        if (_isDragged) return;
        base.OnMouseExit();
    }

    private void Update()
    {
        if (!_isDragged) return;
        Drag(Mouse.current.delta.ReadValue() * dragFactor);
        Mouse.current.WarpCursorPosition(Camera.main.WorldToScreenPoint(_anchor.position));
        // Analytics
        AnalyticsTotalTimeDragging += Time.deltaTime;
    }

    protected virtual void Drag(Vector2 delta)
    {
        Debug.Log(delta);
    }
}
