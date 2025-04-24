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
    private CursorMoveCamera _cursorMoveCamera;
    public float travelSpeed;
    public AnimationCurve movementCurve;
    public Station currentStation;
    
    public static event Action<Station> OnDestinationReached;
    
    private void Awake()
    {
        _cursorMoveCamera = GetComponent<CursorMoveCamera>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = currentStation.transform.position;
        transform.rotation = currentStation.transform.rotation;
    }

    public void MoveDirection(TravelDirection direction)
    {
        StartCoroutine(MoveAlongTravelPath(currentStation.GetPath(direction)));
    }

    private IEnumerator MoveAlongTravelPath(TravelPath path)
    {
        _cursorMoveCamera.canMove = false;
        Cursor.visible = false;
        
        yield return StartCoroutine(Move(path.splineContainer));
        
        currentStation = path.endStation;
        Cursor.visible = true;
        Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
        _cursorMoveCamera.canMove = true;
        transform.position = currentStation.transform.position;
        transform.rotation = currentStation.transform.rotation;
        OnDestinationReached?.Invoke(currentStation);
    }

    private IEnumerator Move(SplineContainer spline)
    {
        float ellapsedTime = 0;
        var lastNode = spline.Spline.Last();
        var length = spline.CalculateLength();
        var totalTime = length / travelSpeed;
        while (ellapsedTime < totalTime)
        {
            ellapsedTime = Mathf.Min(ellapsedTime+Time.deltaTime, totalTime);
            float ratio = ellapsedTime / totalTime;
            transform.position = spline.EvaluatePosition(movementCurve.Evaluate(ratio));
            var tangent = Quaternion.LookRotation(spline.EvaluateTangent(ratio));
            transform.LookAt(lastNode.Position);
            transform.rotation = Quaternion.Slerp(tangent, transform.rotation, ratio);
            yield return new WaitForEndOfFrame();
        }
        // Snap to destination
        transform.position = spline.EvaluatePosition(1f);
        transform.rotation = lastNode.Rotation;
    }
}
