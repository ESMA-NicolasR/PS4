using UnityEngine;

public class ResourceHandle : MonoBehaviour
{
    public ResourceSystem resourceSystem;

    public void ChangeValue(float delta)
    {
        resourceSystem.ChangeValue(delta);
    }
    
    public void SetValue(float newValue)
    {
        resourceSystem.SetValue(newValue);
    }
}
