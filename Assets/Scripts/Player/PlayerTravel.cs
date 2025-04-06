using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTravel : MonoBehaviour
{
    public Transform currentNode;
    private Transform _targetNode;
    private CursorMoveCamera _cursorMoveCamera;
    public float timeToTravel;
    private float _moveSpeed;
    private float _rotateSpeed;

    private void Awake()
    {
        _cursorMoveCamera = GetComponent<CursorMoveCamera>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = currentNode.position;
        transform.rotation = currentNode.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Move if we need to
        if (_targetNode)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetNode.position, Time.deltaTime * _moveSpeed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetNode.rotation, Time.deltaTime * _rotateSpeed);
            // When we're close enough, snap to position and rotation
            if (Vector3.Distance(transform.position, _targetNode.position) < 0.1f)
            {
                currentNode = _targetNode;
                _targetNode = null;
                transform.position = currentNode.position;
                transform.rotation = currentNode.rotation;
                Cursor.visible = true;
                Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
                _cursorMoveCamera.canMove = true;
            }
        }

    }

    public void MoveToNode(Transform node)
    {
        _cursorMoveCamera.canMove = false;
        // _cursorMoveCamera.Reset();
        Cursor.visible = false;
        _targetNode = node;
        var distance = Vector3.Distance(transform.position, node.position);
        var angle = Quaternion.Angle(transform.rotation, node.rotation);
        _moveSpeed = distance / timeToTravel;
        _rotateSpeed = angle / timeToTravel;
    }
}
