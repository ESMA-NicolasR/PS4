using UnityEngine;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField]
    protected ResourceSystem _resourceSystem;
    
    void Start()
    {
        ResourceSystem.OnChangeValue += OnChangeValue;
        UpdateDisplay();
    }
    
    private void OnChangeValue(ResourceSystem resourceSystem)
    {
        if (resourceSystem.name == _resourceSystem.name)
        {
            UpdateDisplay();
        }
    }

    protected virtual void UpdateDisplay()
    {
        Debug.Log($"{_resourceSystem.name}: {_resourceSystem.currentValue}");
    }
}
