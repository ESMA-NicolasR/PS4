using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;
using Cursor = UnityEngine.Cursor;

public class PlayerTravel : MonoBehaviour
{
    [Header("Dependencies")]
    [Tooltip("Station the player is at")]
    public Station currentStation;
    [Tooltip("What will be rotated when moving")]
    public Transform headPivot;
    [Header("Tweaking")]
    [Tooltip("Average speed when moving")]
    public float travelSpeed;
    [Tooltip("How much the destination weight on where to look")]
    public AnimationCurve lookDestinationCurve;
    [Tooltip("How fast the player will turn to the path")]
    public float speedToAdhereToLookAhead;
    [Tooltip("Influence of turning to the path")]
    public AnimationCurve adhereToLookAheadCurve;
    [Tooltip("How the player moves from point A to B")]
    public AnimationCurve movementCurve;
    // Used for internal animations
    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public float timeToArrive;
    // Delegates
    public static event Action<Station> OnDestinationReached;
    public static event Action OnTravelStart;
    // Internal components
    private CursorMoveCamera _cursorMoveCamera;
    
    private void Awake()
    {
        _cursorMoveCamera = GetComponent<CursorMoveCamera>();
    }

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
        headPivot.rotation = _cursorMoveCamera.playerHead.transform.rotation;
        _cursorMoveCamera.ResetCamera();
        // Disable controls
        _cursorMoveCamera.canMove = false;
        Cursor.visible = false;
        currentSpeed = 0;
        OnTravelStart?.Invoke();
        
        // Do the traveling
        yield return StartCoroutine(Move(path.splineContainer));
        
        // Give back control and reset cursor
        currentStation = path.endStation;
        Cursor.visible = true;
        Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
        _cursorMoveCamera.canMove = true;
        
        // Snap to destination
        transform.position = currentStation.transform.position;
        headPivot.rotation = currentStation.transform.rotation;
        currentSpeed = 0;
        timeToArrive = 0;
        OnDestinationReached?.Invoke(currentStation);
    }

    private IEnumerator Move(SplineContainer spline)
    {
        float elapsedTime = 0;
        var lastNode = spline.Spline.Last();
        var length = spline.CalculateLength();
        var totalTime = length / travelSpeed;
        timeToArrive = totalTime;
        // We need to know hom much time is necessary to face the path
        var lookAheadTime = Quaternion.Angle(headPivot.rotation, Quaternion.LookRotation(spline.EvaluateTangent(0)))/speedToAdhereToLookAhead;
        // We eventually want to look at what's right in front of the last node
        Vector3 aimDestination = (Vector3)lastNode.Position + new Quaternion(lastNode.Rotation.value.x,
            lastNode.Rotation.value.y, lastNode.Rotation.value.z, lastNode.Rotation.value.w) * Vector3.forward;
        while (elapsedTime < totalTime)
        {
            elapsedTime = Mathf.Min(elapsedTime+Time.deltaTime, totalTime);
            float ratio = elapsedTime / totalTime;
            // Move along spline
            transform.position = spline.EvaluatePosition(movementCurve.Evaluate(ratio));
            // Compute speed with f'(x) ~= (f(x+dt)-f(x))/dt
            float dt = 0.01f;
            currentSpeed = (movementCurve.Evaluate(ratio+dt)-movementCurve.Evaluate(ratio))/dt;
            timeToArrive -= Time.deltaTime;
            // Look ahead towards the destination
            var tangent = Quaternion.LookRotation(spline.EvaluateTangent(ratio));
            var lookDestination = Quaternion.LookRotation(aimDestination-transform.position);
            // Interpolate between path direction and destination
            var wantedRotation = Quaternion.Slerp(tangent, lookDestination, lookDestinationCurve.Evaluate(ratio));
            // Take into account initial turning from station
            headPivot.rotation = Quaternion.Slerp(headPivot.rotation, wantedRotation, adhereToLookAheadCurve.Evaluate(elapsedTime/lookAheadTime));
            // Wait next frame
            yield return new WaitForEndOfFrame();
        }
    }
}
