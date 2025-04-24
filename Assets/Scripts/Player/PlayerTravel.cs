using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerTravel : MonoBehaviour
{
    public Transform currentNode;
    private CursorMoveCamera _cursorMoveCamera;
    private SplineAnimate _splineAnimate;
    public float timeToTravel;
    
    private void Awake()
    {
        _cursorMoveCamera = GetComponent<CursorMoveCamera>();
        _splineAnimate = GetComponent<SplineAnimate>();
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

    public void MoveAlongSpline(SplineContainer spline)
    {
        StartCoroutine(Move(spline));
    }

    private IEnumerator Move(SplineContainer spline)
    {
        _cursorMoveCamera.canMove = false;
        Cursor.visible = false;
        _splineAnimate.Container = spline;
        float ellapsedTime = 0;
        var firstNode = spline.Spline.First();
        var lastNode = spline.Spline.Last();
        _splineAnimate.Play();
        while (_splineAnimate.IsPlaying)
        {
            ellapsedTime += Time.deltaTime;
            float ratio = ellapsedTime / _splineAnimate.Duration;
            transform.rotation = Quaternion.Slerp(firstNode.Rotation, lastNode.Rotation, ratio);
            yield return new WaitForEndOfFrame();
        }
        // Snap to destination
        transform.position = lastNode.Position;
        transform.rotation = lastNode.Rotation;
        
        Cursor.visible = true;
        Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
        _cursorMoveCamera.canMove = true;
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
