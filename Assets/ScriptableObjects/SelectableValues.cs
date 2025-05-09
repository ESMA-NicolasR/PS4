using System;
using UnityEngine;

public class SelectableValues : ScriptableObject
{
    public virtual Type enumType
    {
        get;
    }

    public String GetValueText(int val)
    {
        return Enum.GetName(enumType, val);
    }
}
