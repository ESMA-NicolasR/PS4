using UnityEngine;

public class ResourceDisplayTextNumber : ResourceDisplayText<ResourceSystemNumber>
{
    [SerializeField] private bool _showOnlyNumber;
    [SerializeField] private string _suffix;
    protected override string GetText()
    {
        if (_showOnlyNumber)
            return _resourceSystem.currentValue.ToString();
        return $"{_resourceSystem.name} level : {_resourceSystem.currentValue} {_suffix}";
    }
}
