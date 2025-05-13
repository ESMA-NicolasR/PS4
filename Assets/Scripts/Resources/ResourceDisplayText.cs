using TMPro;
using UnityEngine;

public class ResourceDisplayText : ResourceDisplay
{
    [SerializeField]
    protected TextMeshPro _text;
    
    void Start()
    {
        ResourceSystem.OnChangeValue += OnChangeValue;
        UpdateDisplay();
    }

    private void OnChangeValue(ResourceSystem resourceSystem)
    {
        if (resourceSystem.name == _resourceSystem.name)
        {
            UpdateDisplay();
        }
    }

    protected override void UpdateDisplay()
    {
        _text.text = GetText();
    }

    protected virtual string GetText()
    {
        return $"{_resourceSystem.name} = {_resourceSystem.currentValue}";
    }
}
