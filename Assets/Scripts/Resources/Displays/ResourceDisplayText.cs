using TMPro;
using UnityEngine;

public class ResourceDisplayText<T> : ResourceDisplay<T> where T : ResourceSystem
{
    [SerializeField]
    protected TextMeshPro _text;
    
    protected override void UpdateDisplay()
    {
        _text.text = GetText();
    }

    protected virtual string GetText()
    {
        return $"{_resourceSystem.name} = {_resourceSystem.currentValue}";
    }
}
