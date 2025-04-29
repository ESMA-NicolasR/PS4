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
    [Header("Tweaking")]
    [Tooltip("Average speed when moving")]
    public float travelSpeed;
    [Tooltip("How much the player will look at its destination")]
    public AnimationCurve lookDestinationCurve;
    [Tooltip("How the player moves from point A to B")]
    public AnimationCurve movementCurve;
    [Tooltip("How fast the player turns before moving")]
    public float lookTowardsPathSpeed;
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
        currentStation.EnableAllInteractables();
    }

    public void MoveDirection(TravelDirection direction)
    {
        StartCoroutine(MoveAlongTravelPath(currentStation.GetPath(direction)));
    }

    private IEnumerator MoveAlongTravelPath(TravelPath path)
    {
        // Give the illusion we move from our current rotation
        transform.rotation = _cursorMoveCamera.playerHead.transform.rotation;
        // Disable controls
        _cursorMoveCamera.canMove = false;
        _cursorMoveCamera.ResetCamera();
        Cursor.visible = false;
        currentSpeed = 0;
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
        currentSpeed = 0;
        timeToArrive = 0;
        OnDestinationReached?.Invoke(currentStation);
    }

    private IEnumerator LookTowardsPathStart(TravelPath path)
    {
        var startTangent = Quaternion.LookRotation(path.splineContainer.Spline.EvaluateTangent(0));
        while (transform.rotation != startTangent)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, startTangent, lookTowardsPathSpeed*Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Move(SplineContainer spline)
    {
        float ellapsedTime = 0;
        var lastNode = spline.Spline.Last();
        var length = spline.CalculateLength();
        var totalTime = length / travelSpeed;
        timeToArrive = totalTime;
        // We eventually want to look at what's right in front of the last node
        Vector3 aimDestination = (Vector3)lastNode.Position + new Quaternion(lastNode.Rotation.value.x,
            lastNode.Rotation.value.y, lastNode.Rotation.value.z, lastNode.Rotation.value.w) * Vector3.forward;
        while (ellapsedTime < totalTime)
        {
            ellapsedTime = Mathf.Min(ellapsedTime+Time.deltaTime, totalTime);
            float ratio = ellapsedTime / totalTime;
            // Move along spline
            transform.position = spline.EvaluatePosition(movementCurve.Evaluate(ratio));
            // Compute speed with f'(x) ~= (f(x+dt)-f(x))/dt
            float dt = 0.01f;
            currentSpeed = (movementCurve.Evaluate(ratio+dt)-movementCurve.Evaluate(ratio))/dt;
            timeToArrive -= Time.deltaTime;
            // Look ahead towards the destination
            var tangent = Quaternion.LookRotation(spline.EvaluateTangent(ratio));
            var lookDestination = Quaternion.LookRotation(aimDestination-transform.position);
            transform.rotation = Quaternion.Slerp(tangent, lookDestination, lookDestinationCurve.Evaluate(ratio));
            // Wait next frame
            yield return new WaitForEndOfFrame();
        }
    }
}
