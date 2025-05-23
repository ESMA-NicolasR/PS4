using UnityEngine;

public class ResourceDisplayTextSwitchRow : ResourceDisplayText<ResourceSystemSwitchRow>
{
    [SerializeField] private string _offValue;
    [SerializeField] private string _partialValue;
    [SerializeField] private string _onValue;

    protected override void UpdateDisplay()
    {
        base.UpdateDisplay();
        Color color = Color.white;
        switch (_resourceSystem.state)
        {
            case SwitchRowState.Off:
                color = Color.red;
                break;
            case SwitchRowState.Partial:
                color = Color.yellow;
                break;
            case SwitchRowState.On:
                color = Color.green;
                break;
        }
        _text.color = color;
    }

    protected override string GetText()
    {
        string text = "";
        switch (_resourceSystem.state)
        {
            case SwitchRowState.Off:
                text = _offValue;
                break;
            case SwitchRowState.Partial:
                text = _partialValue;
                break;
            case SwitchRowState.On:
                text = _onValue;
                break;
        }
        return $"{_resourceSystem.name} state : {text} ";
    }
}
