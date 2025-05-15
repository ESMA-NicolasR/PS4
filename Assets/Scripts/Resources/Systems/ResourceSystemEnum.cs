using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ResourceSystemEnum : ResourceSystem
{
    [SerializeField]
    private SelectEnums _enum;
    private Type _usedEnum;

    private void Awake()
    {
        switch (_enum)
        {
            case SelectEnums.CoordinatesRecipients:
                _usedEnum = typeof(CoordinatesRecipient);
                break;
            case SelectEnums.FluidsNames:
                _usedEnum = typeof(FluidsName);
                break;
        }
    }

    public string GetCurrentValueAsText()
    {
        return Enum.GetName(_usedEnum, currentValue);
    }

    protected override int SanitizeValue(int value)
    {
        // Maximum is the amount of values in the enum
        return Math.Clamp(value, 0, Enum.GetValues(_usedEnum).Length-1);
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        if (_enum == SelectEnums.None)
        {
            throw new ArgumentException($"ResourceSystemEnum {gameObject.name} needs a valid SelectEnum", "SelectEnum");
        }
    }
}
