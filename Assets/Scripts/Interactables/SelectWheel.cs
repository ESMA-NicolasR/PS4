using UnityEngine;

public class SelectWheel : Draggable
{
    public int nbSections;
    public int totalAngles;
    public Transform target;
    public float turnSpeed;
    public ResourceHandle resourceHandle;

    private int _amplitudeMax;
    private int _amplitudePerSection;
    private int _step;
    private int _zeroAngle;
    private float _currentAngle;
    public int currentValue;

    protected override void Start()
    {
        _amplitudeMax = totalAngles / 2;
        _amplitudePerSection = totalAngles / nbSections;
        _step = totalAngles / nbSections;
        _zeroAngle = -_step*(nbSections-1)/2;
        _amplitudeMax = -_zeroAngle;
    }

    protected override void Drag(Vector2 delta)
    {
        _currentAngle = Mathf.Clamp(_currentAngle + delta.x * turnSpeed, -_amplitudeMax, _amplitudeMax);
        currentValue = Mathf.RoundToInt(_currentAngle - _zeroAngle)/_amplitudePerSection ;
        Debug.Log(currentValue);
        target.eulerAngles = Vector3.forward * (currentValue * _amplitudePerSection + _zeroAngle);
        UpdateValue();
    }

    private void UpdateValue()
    {
        resourceHandle.SetValue(currentValue);
    }
    
}
