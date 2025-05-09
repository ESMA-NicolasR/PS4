using System;
using UnityEngine;

public class ResourceDisplayTextBool : ResourceDisplayText<bool, ResourceSystemBool>
{
    [SerializeField] private string _onValue;
    [SerializeField] private string _offValue;
    
    protected override string GetText()
    {
        return $"{_resourceSystem.name} state : {(_resourceSystem.currentValue?_offValue:_onValue)} ";
    }
    
    private void OnValidate()
    {
        if (!_resourceSystem is ResourceSystemBool)
        {
            throw new ArgumentException("ResourceSystem is not a ResourceSystemInteger.");
        }
    }
}
