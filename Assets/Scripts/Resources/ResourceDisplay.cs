using UnityEngine;

public class ResourceDisplay<T,U> : MonoBehaviour where U : ResourceSystem<T>
{
    [SerializeField]
    protected U _resourceSystem;
    
    private void OnEnable()
    {
        _resourceSystem.OnChangeValue += UpdateDisplay;
    }

    private void OnDisable()
    {
        _resourceSystem.OnChangeValue -= UpdateDisplay;
    }

    private void Start()
    {
        UpdateDisplay();
    }

    protected virtual void UpdateDisplay()
    {
        Debug.Log($"{_resourceSystem.name}: {_resourceSystem.currentValue}");
    }
}
