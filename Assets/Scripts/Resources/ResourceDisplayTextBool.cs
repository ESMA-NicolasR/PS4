using TMPro;
using UnityEngine;

public class ResourceDisplayTextBool : ResourceDisplayText
{
    [SerializeField] private string _onValue;
    [SerializeField] private string _offValue;


    protected override string GetText()
    {
        return $"{_resourceSystem.name} state : {(_resourceSystem.currentValue==0?_offValue:_onValue)} ";
    }
}
