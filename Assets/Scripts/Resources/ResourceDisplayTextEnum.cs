using System;
using TMPro;
using UnityEngine;

public class ResourceDisplayTextEnum : ResourceDisplayText<int, ResourceSystemEnum>
{
    protected override string GetText()
    {
        return $"{_resourceSystem.name} selected : {_resourceSystem.GetCurrentValueAsText()}";
    }
}
