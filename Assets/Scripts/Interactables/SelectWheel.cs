using UnityEngine;

public class SelectWheel : Draggable
{
    public int nbSections;
    public int totalAngles;
    public Transform target;
    public ResourceHandle resourceHandle;

    private int _amplitudeMax;
    private int _amplitudePerSection;
    private int _step;
    private int _zeroAngle;
    private float _currentAngle;
    public int currentValue;

    protected override void Start()
    {
        _amplitudeMax = totalAngles / 2-1;
        _amplitudePerSection = totalAngles / nbSections;
        _step = totalAngles / nbSections;
        _zeroAngle = Mathf.FloorToInt(-_step*(nbSections-1)/2.0f);
        currentValue = resourceHandle.GetCurrentValue();
        TurnToCurrentValue();
    }

    protected override void Drag(Vector2 delta)
    {
        _currentAngle = Mathf.Clamp(_currentAngle + delta.x, -_amplitudeMax, _amplitudeMax);
        currentValue = Mathf.RoundToInt(_currentAngle + _amplitudeMax)/_amplitudePerSection ;
        TurnToCurrentValue();
        UpdateValue();
    }

    private void TurnToCurrentValue()
    {
        target.localEulerAngles = Vector3.forward * (currentValue * _amplitudePerSection + _zeroAngle);
    }

    private void UpdateValue()
    {
        resourceHandle.SetValue(currentValue);
    }
    
}
