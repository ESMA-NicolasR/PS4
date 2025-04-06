using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTravel : MonoBehaviour
{
    public Transform currentNode;
    private CursorMoveCamera _cursorMoveCamera;
    public float timeToTravel;
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

    public void MoveToNode(Transform node)
    {
        StartCoroutine(Move(currentNode, node));
    }

    private IEnumerator Move(Transform from, Transform to)
    {
        _cursorMoveCamera.canMove = false;
        Cursor.visible = false;
        //
        float ellapsedTime = 0;
        while (ellapsedTime < timeToTravel)
        {
            ellapsedTime += Time.deltaTime;
            float ratio = ellapsedTime / timeToTravel;
            transform.position = Vector3.Slerp(from.position, to.position, ratio);
            transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, ratio);
            yield return new WaitForEndOfFrame();
        }
        // Snap to the desired coordinates
        transform.position = to.position;
        transform.rotation = to.rotation;
        //
        
        currentNode = to;
        Cursor.visible = true;
        Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
        _cursorMoveCamera.canMove = true;

    }
}
