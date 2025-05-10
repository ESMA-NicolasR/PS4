using UnityEngine;

public class ResourceDisplayTextBool : ResourceDisplayText<ResourceSystemBool>
{
    [SerializeField] private string _onValue;
    [SerializeField] private string _offValue;
    
    protected override string GetText()
    {
        return $"{_resourceSystem.name} state : {(_resourceSystem.GetCurrentValueAsBool()?_onValue:_offValue)} ";
    }
}
