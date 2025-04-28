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
    public int step;
    public int minDistance;
    public int maxDistance;
    
    public static event Action<ResourceSystem> OnChangeValue;
    
    public virtual void Break()
    {
        targetValue = SanitizeValue(Random.Range(minValue+step, maxValue-step));
        int randomSign = Random.Range(0, 2) * 2 - 1;
        int randomDistance = Random.Range(step * minDistance, step * maxDistance);
        SetValue(targetValue + randomSign*randomDistance);
    }

    public virtual void ChangeValue(int delta)
    {
        SetValue(currentValue+delta*step);
    }
    
    public virtual void SetValue(int newValue)
    {
        currentValue = SanitizeValue(newValue);
        OnChangeValue?.Invoke(this);
    }

    protected virtual int SanitizeValue(int value)
    {
        return Math.Clamp(value - value % step, minValue, maxValue);
    }
}
