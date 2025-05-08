using UnityEngine;

public class ResourceHandle : MonoBehaviour
{
    public ResourceSystem resourceSystem;

    public int GetCurrentValue()
    {
        return resourceSystem.currentValue;
    }

    public void ChangeValue(int delta)
    {
        resourceSystem.ChangeValue(delta);
    }
    
    public void SetValue(int newValue)
    {
        resourceSystem.SetValue(newValue);
    }
}
