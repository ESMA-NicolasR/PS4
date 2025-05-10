using UnityEngine;

public class ResourceDisplayLight : ResourceDisplay<ResourceSystemNumber>
{
    [SerializeField]
    private Light _light;

    protected override void UpdateDisplay()
    {
        _light.intensity = (float)_resourceSystem.currentValue/_resourceSystem.maxValue;
    }
}
