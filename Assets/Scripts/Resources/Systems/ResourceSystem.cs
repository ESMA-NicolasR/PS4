using System;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    public SystemName systemName;
    public int currentValue;
    public int targetValue;
    
    public event Action OnChangeValue;
    
    public virtual void Break()
    {
        throw new NotImplementedException();
    }
    
    public void Break(int newTargetValue, int breakValue)
    {
        SetTargetValue(newTargetValue);
        SetValue(breakValue);
    }

    public virtual void ChangeValue(int delta)
    {
        SetValue(currentValue);
    }
    
    public virtual void SetValue(int newValue)
    {
        currentValue = SanitizeValue(newValue);
        OnChangeValue?.Invoke();
    }

    private void SetTargetValue(int newValue)
    {
        targetValue = SanitizeValue(newValue);
    }

    protected virtual int SanitizeValue(int value)
    {
        return value;
    }

    public bool IsFixed()
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
