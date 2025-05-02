using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Draggable : Clickable
{
    private bool _isDragged;
    [SerializeField]
    private Transform _anchor;
    public float dragFactor;
    
    protected override void Interact()
    {
        _isDragged = true;
        //_startPosition = Mouse.current.position.ReadValue();
        Cursor.visible = false;
        FindFirstObjectByType<CursorMoveCamera>().canMove = false;
    }

    private void OnMouseUp()
    {
        //Mouse.current.WarpCursorPosition(_anchor.position);
        Cursor.visible = true;
        _isDragged = false;
        FindFirstObjectByType<CursorMoveCamera>().canMove = true;
    }

    private void Update()
    {
        if (!_isDragged) return;
        Drag(Mouse.current.delta.ReadValue() * dragFactor);
        Mouse.current.WarpCursorPosition(Camera.main.WorldToScreenPoint(_anchor.position));

    }

    protected virtual void Drag(Vector2 delta)
    {
        Debug.Log(delta);
    }
}
