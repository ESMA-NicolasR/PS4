using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FluidsScenarioData", menuName = "Data/FluidsScenarioData")]
public class FluidsScenarioData : ResourceObjectiveData
{
    public List<int> targetValues;

    public override void BreakSystem(ResourceSystem resourceSystem)
    {
        var systemNetwork = resourceSystem as ResourceSystemFluids;
        if (systemNetwork == null)
        {
            throw new Exception($"ResourceSystem of FluidsScenarioData {name} must be a ResourceSystemFluids");
        }
        systemNetwork.Break(this);
    }
}
