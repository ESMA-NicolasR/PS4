using System;
using UnityEngine;

public class ResourceObjective : MonoBehaviour
{
    [SerializeField]
    private ResourceSystem _resourceSystem;

    public static event Action OnObjectiveCompleted;
    void Start()
    {
        ResourceSystem.OnChangeValue += OnChangeValue;
    }

    public void CreateObjective()
    {
        _resourceSystem.Break();
    }

    private void OnChangeValue(ResourceSystem resourceSystem)
    {
        if (_resourceSystem.resourceName == resourceSystem.resourceName)
        {
            CheckIsCompleted();
        }
    }

    private void CheckIsCompleted()
    {
        if (_resourceSystem.currentValue == _resourceSystem.targetValue)
        {
            OnObjectiveCompleted?.Invoke();
        }
    }

    public string GetDescription()
    {
        return $"Set the {_resourceSystem.resourceName} to {_resourceSystem.targetValue}";
    }
}
