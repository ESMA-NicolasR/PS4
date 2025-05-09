using TMPro;
using UnityEngine;

public class ResourceDisplayTextNumber : ResourceDisplayText<int, ResourceSystemInteger>
{
    [SerializeField] private string _suffix;
    
    protected override void UpdateDisplay()
    {
        base.UpdateDisplay();
        var distance = Mathf.Abs(_resourceSystem.targetValue - _resourceSystem.currentValue);
        _text.color = Color.Lerp(Color.green, Color.red, Mathf.Pow((float)distance/(_resourceSystem.maxValue-_resourceSystem.minValue),0.3f));
    }

    protected override string GetText()
    {
        return $"{_resourceSystem.name} level : {_resourceSystem.currentValue} {_suffix}";
    }
}
