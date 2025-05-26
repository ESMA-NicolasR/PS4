using UnityEngine;

public class SelectWheel : Draggable
{
    public int nbSections;
    public int totalAngles;
    public Transform target;
    public ResourceSystem resourceSystem;
    protected override CursorType cursorType => CursorType.LeftRight;

    private int _amplitudeMax;
    private int _amplitudePerSection;
    private int _step;
    private int _zeroAngle;
    private float _currentAngle;
    private int _displayedValue;

    protected override void Start()
    {
        _amplitudeMax = totalAngles / 2-1;
        _amplitudePerSection = totalAngles / nbSections;
        _step = totalAngles / nbSections;
        _zeroAngle = Mathf.FloorToInt(-_step*(nbSections-1)/2.0f);
        _displayedValue = -1; // Force update display for the first frame
        UpdateDisplay();
    }
    
    private void OnEnable()
    {
        resourceSystem.OnChangeValue += UpdateDisplay;
    }
    
    private void OnDisable()
    {
        resourceSystem.OnChangeValue -= UpdateDisplay;
    }

    protected override void Drag(Vector2 delta)
    {
        _currentAngle = Mathf.Clamp(_currentAngle + delta.x, -_amplitudeMax, _amplitudeMax);
        UpdateValue();
    }


    private void UpdateValue()
    {
        resourceSystem.SetValue(Mathf.RoundToInt(_currentAngle + _amplitudeMax) / _amplitudePerSection);
    }

    private void UpdateDisplay()
    {
        // Don't turn until we really changed value
        if (_displayedValue != resourceSystem.currentValue)
        {
            _displayedValue = resourceSystem.currentValue;
            _currentAngle = _displayedValue * _amplitudePerSection + _zeroAngle;
            target.localEulerAngles = Vector3.forward * _currentAngle;
            if(canBeUsed)
                feedbackSound?.PlayMySound();
        }
    }
}
