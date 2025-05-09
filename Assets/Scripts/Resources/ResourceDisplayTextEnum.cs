using System;
using TMPro;
using UnityEngine;

public class ResourceDisplayTextEnum : ResourceDisplayText
{
    [SerializeField]
    public SelectableValues enumToDisplay;
    
    [SerializeField]
    private string _infix;

    protected override string GetText()
    {
        return $"{_resourceSystem.name} {_infix}selected : {enumToDisplay.GetValueText(_resourceSystem.currentValue)}";
    }
}
