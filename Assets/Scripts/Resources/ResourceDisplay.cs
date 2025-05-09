using System;
using UnityEngine;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField]
    protected ResourceSystem _resourceSystem;

    private void OnEnable()
    {
        ResourceSystem.OnChangeValue += OnChangeValue;
    }

    private void OnDisable()
    {
        ResourceSystem.OnChangeValue -= OnChangeValue;
    }

    void Start()
    {
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
        Debug.Log($"{_resourceSystem.name}: {_resourceSystem.currentValue}");
    }
}
