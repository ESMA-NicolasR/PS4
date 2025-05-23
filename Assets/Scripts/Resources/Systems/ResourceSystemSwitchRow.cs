using System;
using System.Collections.Generic;
using System.Linq;

public class ResourceSystemSwitchRow : ResourceSystem
{
    public List<ResourceSystemBool> subsystems;
    public SwitchRowState state;

    private void Awake()
    {
        foreach (var systemBool in subsystems)
        {
            systemBool.OnChangeValue += OnSubsystemValueChange;
        }
    }

    public override void Break(int newTargetValue, int breakValue)
    {
        base.Break(newTargetValue, breakValue);
        var remainingSwitchs = breakValue;
        foreach (var systemBool in subsystems)
        {
            systemBool.SetValue(remainingSwitchs-- > 0);
        }
    }

    private void OnSubsystemValueChange()
    {
        int nbOn = subsystems.Count(x => x.GetCurrentValueAsBool());
        // Update state
        if (nbOn == 0)
        {
            state = SwitchRowState.Off;
        }
        else if (nbOn == subsystems.Count)
        {
            state = SwitchRowState.On;
        }
        else
        {
            state = SwitchRowState.Partial;
        }
        // Change value after the state so it's read in order 
        SetValue(nbOn);
    }

    protected override int SanitizeValue(int value)
    {
        return Math.Clamp(value, 0, subsystems.Count);
    }
}
