using System;
using UnityEngine;

public class ResourceSystem<T> : MonoBehaviour
{
    public string resourceName;
    public T currentValue;
    public T targetValue;
    
    public event Action OnChangeValue;
    
    public virtual void Break()
    {
        throw new NotImplementedException();
    }

    public virtual void ChangeValue(T delta)
    {
        SetValue(currentValue);
    }
    
    public virtual void SetValue(T newValue)
    {
        currentValue = SanitizeValue(newValue);
        OnChangeValue?.Invoke();
    }

    protected virtual T SanitizeValue(T value)
    {
        return value;
    }

    public bool IsFixed()
    {
        return currentValue.Equals(targetValue);
    }
}
