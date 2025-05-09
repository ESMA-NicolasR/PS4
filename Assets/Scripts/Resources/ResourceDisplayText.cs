using TMPro;
using UnityEngine;

public class ResourceDisplayText<T, U> : ResourceDisplay<T, U> where U : ResourceSystem<T>
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
