using UnityEngine;

public class Switch : Clickable
{
    private bool _isToggled;
    public Transform target;
    public float rotationAmplitude;
    public ResourceSystemBool resourceSystem;

    protected override void Start()
    {
        _isToggled = resourceSystem.currentValue;
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
        resourceSystem.SetValue(_isToggled);
    }
}
