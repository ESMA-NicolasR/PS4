using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ResourceSystemEnum : ResourceSystem
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
        if (_enum == SelectEnums.None)
        {
            throw new ArgumentNullException("SelectEnum", $"ResourceSystemEnum {gameObject.name} needs a valid SelectEnum");
        }

        switch (_enum)
        {
            case SelectEnums.CoordinatesRecipients:
                _usedEnum = typeof(CoordinatesRecipient);
                break;
        }
    }
}
