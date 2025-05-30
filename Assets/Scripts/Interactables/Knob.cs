using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class Knob : Draggable
{
    public Transform target;
    private float _startAngleTarget;
    private float _startAngleCursor;
    public Transform fakeCursor;
    public Transform handlePivot;
    public SplineContainer circle;
    public float turnSpeed;
    // How much degrees are needed to change value
    public int degreesForValue;
    // How much the value changes at a time
    public int valueStrength;
    
    // Dependencies
    public ResourceSystemNumber resourceSystem;

    // Internal variables
    private float _lastAngle;
    private float _progress;
    [SerializeField]
    private float _minProgress;
    [SerializeField]
    private float _maxProgress;

    private float _time;
    private float _timeBeforePlayingAgain;
    private bool _isPlayingSound = false;
    protected override CursorType cursorType => CursorType.Circle;

    private void OnEnable()
    {
        resourceSystem.OnChangeValue += OnSystemChangeValue;
    }
    
    private void OnDisable()
    {
        resourceSystem.OnChangeValue -= OnSystemChangeValue;
    }


    protected override void Start()
    {
        _progress = resourceSystem.currentValue;
        // Setup fake cursor
        fakeCursor.position = handlePivot.position;
        SnapCursorToCircle(fakeCursor.position);
    }

    protected override void Interact()
    {
        base.Interact();

        // Set up rotation
        Vector3 vector = fakeCursor.localPosition;
        _startAngleCursor = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        _startAngleTarget = target.localEulerAngles.z;
    }

    protected override void Drag(Vector2 delta)
    {
        _lastAngle = target.localEulerAngles.z;
        // Update fake cursor position
        fakeCursor.localPosition += turnSpeed*Time.deltaTime*new Vector3(-delta.x, delta.y, 0f);
        SnapCursorToCircle(fakeCursor.position);
        // Rotate according to fake cursor position        
        Vector3 vector = fakeCursor.localPosition;
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        target.localEulerAngles = new Vector3(0, 0, _startAngleTarget + angle-_startAngleCursor);
        UpdateProgress(Mathf.DeltaAngle(_lastAngle, target.localEulerAngles.z));
        if (delta.magnitude > 0)
        {
            PlayNoise();
        }
    }
    
    private void SnapCursorToCircle(Vector3 worldPosition)
    {
        Vector3 localSplinePoint = circle.transform.InverseTransformPoint(worldPosition);
        SplineUtility.GetNearestPoint(circle.Spline, localSplinePoint, out float3 nearestPoint, out float _);
        Vector3 nearestWorldPosition = circle.transform.TransformPoint(nearestPoint);
        fakeCursor.position = nearestWorldPosition;
    }

    private void UpdateProgress(float delta)
    {
        _progress = Mathf.Clamp(_progress+delta/degreesForValue*valueStrength, _minProgress, _maxProgress);
        resourceSystem.SetValue(
            Mathf.RoundToInt(_progress)
        );
    }

    private void OnSystemChangeValue()
    {
        // Only take the update if the rounded value changes
        if(Mathf.RoundToInt(_progress)!=resourceSystem.currentValue)
            _progress = resourceSystem.currentValue;
    }

    private void PlayNoise()
    {
        _time = Time.time;
        if (!_isPlayingSound)
        {
            _isPlayingSound = true;
            feedbackSound?.PlayMySound();
            _timeBeforePlayingAgain = Time.time + 1f;
            StartCoroutine(CheckIfSoundIsStillPlaying());
        }
    }

    private IEnumerator CheckIfSoundIsStillPlaying()
    {
        while (_isPlayingSound)
        {
            yield return new WaitForSeconds(0.1f);
            if (Time.time > _time+0.1f)
            {
                _isPlayingSound = false;
                feedbackSound?.StopAllSounds();
            }
            else if( _timeBeforePlayingAgain < Time.time)
            {
                feedbackSound?.PlayMySound();
                _timeBeforePlayingAgain = Time.time + 1f;
            }
        }
    }
}
