using UnityEngine;

public class Switch : Clickable
{
    private bool _isToggled;
    public Transform target;
    public float rotationAmplitude;
    public ResourceHandle resourceHandle;
    public int valueOn;
    public int valueOff;

    protected override void Start()
    {
        _isToggled = resourceHandle.GetCurrentValue() != 0;
        RotateWithToggle();
    }

    protected override void Interact()
    {
        Toggle();
    }

    public void Toggle()
    {
        _isToggled = !_isToggled;
        RotateWithToggle();
        UpdateValue();
    }

    private void RotateWithToggle()
    {
        target.localEulerAngles = new Vector3(rotationAmplitude*(_isToggled?1:-1), 0, 0);
    }

    private void UpdateValue()
    {
        resourceHandle.SetValue(_isToggled?valueOn:valueOff);
    }
}
