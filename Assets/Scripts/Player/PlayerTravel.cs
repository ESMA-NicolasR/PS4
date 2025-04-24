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
    public float lookDestinationStrength;
    public AnimationCurve movementCurve;
    public Station currentStation;
    public float lookTowardsPathSpeed;
    
    public static event Action<Station> OnDestinationReached;
    public static event Action OnTravelStart;
    
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
        // Give the illusion we move from our current rotation
        transform.rotation = _cursorMoveCamera.playerCamera.transform.rotation;
        // Disable controls
        _cursorMoveCamera.canMove = false;
        _cursorMoveCamera.ResetCamera();
        Cursor.visible = false;
        OnTravelStart?.Invoke();
        
        // Turn towards spline start
        yield return StartCoroutine(LookTowardsPathStart(path));
        
        // Do the traveling
        yield return StartCoroutine(Move(path.splineContainer));
        
        // Give back control and reset cursor
        currentStation = path.endStation;
        Cursor.visible = true;
        Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
        _cursorMoveCamera.canMove = true;
        // Snap to destination
        transform.position = currentStation.transform.position;
        transform.rotation = currentStation.transform.rotation;
        OnDestinationReached?.Invoke(currentStation);
    }

    private IEnumerator LookTowardsPathStart(TravelPath path)
    {
        var tmpRotation = transform.rotation;
        var tangent = Quaternion.LookRotation(path.splineContainer.Spline.EvaluateTangent(0));
        transform.LookAt(path.splineContainer.Spline.Last().Position);
        var targetRotation = Quaternion.Slerp(tangent, transform.rotation, lookDestinationStrength);
        transform.rotation = tmpRotation;
        while (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, lookTowardsPathSpeed*Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
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
            transform.rotation = Quaternion.Slerp(tangent, transform.rotation, lookDestinationStrength);
            yield return new WaitForEndOfFrame();
        }
    }
}
