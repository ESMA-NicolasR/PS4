using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystemSwitchRow : ResourceSystem
{
    public List<ResourceSystemBool> subsystems;
    public SwitchRowState state;

    [Header("Feedbacks")]
    [SerializeField] private FeedbackSound _feedbackShieldsUp;
    [SerializeField] private FeedbackSound _feedbackShieldsDown;
    
    [SerializeField]
    private Station _station;
    
    
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
            if (PlayerTravel.Instance.currentStation == _station)
            {
                _feedbackShieldsDown.PlayMySound();
            }
        }
        else if (nbOn == subsystems.Count)
        {
            state = SwitchRowState.On;
            if (PlayerTravel.Instance.currentStation == _station)
            {
                _feedbackShieldsUp.PlayMySound();
            }
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
