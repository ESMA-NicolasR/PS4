using UnityEngine;

public class ResourceObjective : MonoBehaviour
{
    public SystemName SystemName;

    public void CreateFromSO(ResourceObjectiveData resourceObjectiveData)
    {
        
    }
    
    public void CreateObjective()
    {
        _resourceSystem.Break();
    }

    public bool CheckIsCompleted()
    {
        return _resourceSystem.IsFixed();
    }

    public string GetDescription()
    {
        return $"Set the {_resourceSystem.resourceName} to {_resourceSystem.targetValue}";
    }
}
