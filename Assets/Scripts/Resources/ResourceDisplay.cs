using TMPro;
using UnityEngine;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField]
    private ResourceSystem _resourceSystem;
    public TextMeshPro text;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    protected virtual void UpdateDisplay()
    {
        text.text = $"{_resourceSystem.name} level : {_resourceSystem.currentValue}";
        text.color = Color.Lerp(Color.red, Color.green, (_resourceSystem.maxValue-(_resourceSystem.targetValue-_resourceSystem.currentValue))/_resourceSystem.maxValue);
    }
}
