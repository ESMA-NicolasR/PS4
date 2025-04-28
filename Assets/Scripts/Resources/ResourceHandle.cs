using UnityEngine;

public class ResourceHandle : MonoBehaviour
{
    public ResourceSystem resourceSystem;

    public void ChangeValue(int delta)
    {
        resourceSystem.ChangeValue(delta);
    }
    
    public void SetValue(int newValue)
    {
        resourceSystem.SetValue(newValue);
    }
}
