using UnityEngine;

public class Switch : Clickable
{
    public Transform target;
    public float rotationAmplitude;
    public ResourceSystemBool resourceSystem;

    private void OnEnable()
    {
        resourceSystem.OnChangeValue += UpdateDisplay;
    }

    private void OnDisable()
    {
        resourceSystem.OnChangeValue -= UpdateDisplay;
    }

    protected override void Start()
    {
        UpdateDisplay();
    }

    protected override void Interact()
    {
        Toggle();
        feedbackSound.PlayMySound();
    }

    public void Toggle()
    {
        UpdateValue();
    }

    private void UpdateDisplay()
    {
        target.localEulerAngles = new Vector3(rotationAmplitude*(resourceSystem.GetCurrentValueAsBool()? 1 : -1), 0, 0);
    }

    private void UpdateValue()
    {
        resourceSystem.Flip();
    }
}
