using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ResourceSystemEnum : ResourceSystem<int>
{
    [SerializeField]
    private SelectEnums _enum;
    private Type _usedEnum;


    public string GetCurrentValueAsText()
    {
        return Enum.GetName(_usedEnum, currentValue);
    }

    private void OnValidate()
    {
        if (_enum != null)
        {
            throw new ArgumentNullException("The enum must be set.");
        }

        switch (_enum)
        {
            case SelectEnums.CoordinatesRecipients:
                _usedEnum = typeof(CoordinatesRecipients);
                break;
        }
    }
}
