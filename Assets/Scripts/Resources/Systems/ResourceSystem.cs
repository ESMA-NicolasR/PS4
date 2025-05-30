using System;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    public SystemName systemName;
    public int currentValue;
    public int targetValue;
    
    public event Action OnChangeValue;

    private void Start()
    {
        // Make sure initial value is ok
        SetValue(currentValue);
    }

    public virtual void Break()
    {
        throw new NotImplementedException();
    }
    
    public virtual void Break(int newTargetValue, int breakValue)
    {
        SetTargetValue(newTargetValue);
        SetValue(breakValue);
    }

    public virtual void ChangeValue(int delta)
    {
        SetValue(currentValue + delta);
    }
    
    public virtual void SetValue(int newValue)
    {
        currentValue = SanitizeValue(newValue);
        OnChangeValue?.Invoke();
    }

    protected void SetTargetValue(int value)
    {
        targetValue = SanitizeValue(value);
    }

    protected virtual int SanitizeValue(int value)
    {
        return value;
    }

    public virtual bool IsFixed()
    {
        return currentValue.Equals(targetValue);
    }

    protected virtual void OnValidate()
    {
        if (systemName == SystemName.None)
        {
            throw new ArgumentException($"ResourceSystem {gameObject.name} needs a valid SystemName", "SystemName");
        }
    }
}
