using System;
using UnityEngine;

public class ResourceDisplayLight : ResourceDisplay<int, ResourceSystemInteger>
{
    [SerializeField]
    private Light _light;

    protected override void UpdateDisplay()
    {
        _light.intensity = (float)_resourceSystem.currentValue/_resourceSystem.maxValue;
    }

    private void OnValidate()
    {
        if (!_resourceSystem is ResourceSystemInteger)
        {
            throw new ArgumentException("ResourceSystem is not a ResourceSystemInteger.");
        }
    }
}
