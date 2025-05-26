using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplayFillbar : ResourceDisplay<ResourceSystemNumber>
{
    public Image fillbar;

    protected override void UpdateDisplay()
    {
        fillbar.fillAmount = Mathf.InverseLerp(
            _resourceSystem.minValue,
            _resourceSystem.maxValue,
            _resourceSystem.currentValue
        );
    }
}
