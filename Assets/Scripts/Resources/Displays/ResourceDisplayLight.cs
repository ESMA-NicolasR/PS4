using UnityEngine;

public class ResourceDisplayLight : ResourceDisplay<ResourceSystemNumber>
{
    [SerializeField]
    private Light _light;
    public float minIntensity;
    public float maxIntensity;

    protected override void UpdateDisplay()
    {
        _light.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.InverseLerp(_resourceSystem.minValue, _resourceSystem.maxValue, _resourceSystem.currentValue));
    }
}
