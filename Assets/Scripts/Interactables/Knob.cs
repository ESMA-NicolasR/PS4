using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class Knob : Draggable
{
    public Transform target;
    private Quaternion originalRotation;
    private float startAngle;
    public MeshRenderer fakeCursor;
    public SplineContainer circle;
    public float turnSpeed;
    public float rotateTransmission;
    // How much degrees are needed to change value
    public int degreesForValue;
    // How much the value changes at a time
    public int valueStrength;
    
    // Dependencies
    public ResourceHandle resourceHandle;

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
        fakeCursor.enabled = false;
        _progress = resourceHandle.GetCurrentValue();
    }

    protected override void Interact()
    {
        base.Interact();
        // Setup fake cursor
        fakeCursor.enabled = true;
        Vector3 startPosition = Input.mousePosition;
        startPosition.z = Vector3.Distance(target.position, Camera.main.transform.position);
        SnapCursorToCircle(Camera.main.ScreenToWorldPoint(startPosition));
        // Set up rotation
        originalRotation = target.rotation;
        Vector3 vector = fakeCursor.transform.localPosition;// - target.position;
        startAngle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        // Hide fake cursor
        fakeCursor.enabled = false;
    }

    protected override void Drag(Vector2 delta)
    {
        _lastAngle = target.eulerAngles.z;
        // Update fake cursor position
        fakeCursor.transform.localPosition += turnSpeed*Time.deltaTime*new Vector3(-delta.x, delta.y, 0f);
        SnapCursorToCircle(fakeCursor.transform.position);
        // Rotate according to fake cursor position        
        Vector3 vector = fakeCursor.transform.localPosition;// - target.position;
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
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
        resourceHandle.SetValue(
            _progress>0
            ?Mathf.FloorToInt(_progress)
            :Mathf.CeilToInt(_progress)
        );
    }
}
