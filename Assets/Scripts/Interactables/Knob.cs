using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class Knob : Draggable
{
    public Transform target;
    private float startAngle;
    public Transform fakeCursor;
    public Transform handlePivot;
    public SplineContainer circle;
    public float turnSpeed;
    public float rotateTransmission;
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


    protected override void Start()
    {
        base.Start();
        // Hide fake cursor
        _progress = resourceSystem.currentValue;
    }

    protected override void Interact()
    {
        base.Interact();
        // Setup fake cursor
        Vector3 startPosition = handlePivot.position;
        SnapCursorToCircle(startPosition);
        // Set up rotation
        Vector3 vector = fakeCursor.transform.localPosition;
        startAngle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    protected override void Drag(Vector2 delta)
    {
        _lastAngle = target.eulerAngles.z;
        // Update fake cursor position
        fakeCursor.transform.localPosition += turnSpeed*Time.deltaTime*new Vector3(-delta.x, delta.y, 0f);
        SnapCursorToCircle(fakeCursor.transform.position);
        // Rotate according to fake cursor position        
        Vector3 vector = fakeCursor.transform.localPosition;
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg * rotateTransmission;
        target.localEulerAngles = new Vector3(0, 0, angle - startAngle);
        UpdateProgress(Mathf.DeltaAngle(_lastAngle, target.eulerAngles.z));
    }
    
    private void SnapCursorToCircle(Vector3 worldPosition)
    {
        Vector3 localSplinePoint = circle.transform.InverseTransformPoint(worldPosition);
        SplineUtility.GetNearestPoint(circle.Spline, localSplinePoint, out float3 nearestPoint, out float _);
        Vector3 nearestWorldPosition = circle.transform.TransformPoint(nearestPoint);
        fakeCursor.transform.position = nearestWorldPosition;
    }

    private void UpdateProgress(float delta)
    {
        _progress = Mathf.Clamp(_progress+delta/degreesForValue*valueStrength, _minProgress, _maxProgress);
        resourceSystem.SetValue(
            _progress>0
            ?Mathf.FloorToInt(_progress)
            :Mathf.CeilToInt(_progress)
        );
    }
}
