using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceSystem : MonoBehaviour
{
    public string resourceName;
    public float currentValue;
    public float targetValue;
    public float minValue;
    public float maxValue;
    
    public static event Action<ResourceSystem> OnChangeValue;
    
    protected virtual void Break()
    {
        currentValue = Random.Range(minValue, maxValue);
    }

    public virtual void ChangeValue(float delta)
    {
        currentValue = Mathf.Clamp(currentValue+delta, minValue, maxValue);
        OnChangeValue?.Invoke(this);
    }
    
    public virtual void SetValue(float newValue)
    {
        currentValue = Mathf.Clamp(newValue, minValue, maxValue);
        OnChangeValue?.Invoke(this);
    }
}
