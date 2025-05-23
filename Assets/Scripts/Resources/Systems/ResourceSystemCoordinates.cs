using System;

public class ResourceSystemCoordinates : ResourceSystem
{
    public ResourceSystemEnum recipientSystem;
    public ResourceSystemNumber xSystem, ySystem, zSystem;

    private void OnEnable()
    {
        recipientSystem.OnChangeValue += OnSubsystemValueChanged;
        xSystem.OnChangeValue += OnSubsystemValueChanged;
        ySystem.OnChangeValue += OnSubsystemValueChanged;
        zSystem.OnChangeValue += OnSubsystemValueChanged;
    }
    
    private void OnDisable()
    {
        recipientSystem.OnChangeValue -= OnSubsystemValueChanged;
        xSystem.OnChangeValue -= OnSubsystemValueChanged;
        ySystem.OnChangeValue -= OnSubsystemValueChanged;
        zSystem.OnChangeValue -= OnSubsystemValueChanged;
    }

    public override bool IsFixed()
    {
        return (
            recipientSystem.IsFixed()
            && xSystem.IsFixed()
            && ySystem.IsFixed()
            && zSystem.IsFixed()
        );
    }

    public void Break(CoordinatesScenarioData scenarioData)
    {
        recipientSystem.Break((int)scenarioData.recipient, 0);
        xSystem.Break(scenarioData.x, 0);
        ySystem.Break(scenarioData.y, 0);
        zSystem.Break(scenarioData.z, 0);
    }

    private void OnSubsystemValueChanged()
    {
        // Will force emitting OnChangeValue event
        SetValue(0);
    }
}
