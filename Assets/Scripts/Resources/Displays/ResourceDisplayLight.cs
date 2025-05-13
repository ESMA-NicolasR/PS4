using UnityEngine;

public class ResourceDisplayLight : ResourceDisplay<ResourceSystemNumber>
{
    [SerializeField]
    private Light _light;
    public float maxIntensity;

    protected override void UpdateDisplay()
    {
        _light.intensity = (float)_resourceSystem.currentValue/_resourceSystem.maxValue * maxIntensity;
    }
}
