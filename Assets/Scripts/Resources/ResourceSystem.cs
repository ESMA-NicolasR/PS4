using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceSystem : MonoBehaviour
{
    public string resourceName;
    public int currentValue;
    public int targetValue;
    public int minValue;
    public int maxValue;
    
    public static event Action<ResourceSystem> OnChangeValue;
    
    public virtual void Break()
    {
        targetValue = Random.Range(minValue, maxValue);
        SetValue(targetValue+Random.Range(10, 50));
    }

    public virtual void ChangeValue(int delta)
    {
        SetValue(currentValue+delta);
    }
    
    public virtual void SetValue(int newValue)
    {
        currentValue = Math.Clamp(newValue-newValue%5, minValue, maxValue);
        OnChangeValue?.Invoke(this);
    }
}
