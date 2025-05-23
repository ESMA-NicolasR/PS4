using System;
using System.Collections.Generic;
using System.Linq;

public class ResourceSystemFluids : ResourceSystem
{
    public List<ResourceSystemNumber> subsystems;

    public void Break(FluidsScenarioData scenarioData)
    {
        if (scenarioData.targetValues.Count != subsystems.Count)
        {
            throw new ArgumentException("FluidsScenarioData target count does not match number of subsystems");
        }

        for (int i = 0; i < subsystems.Count; i++)
        {
            subsystems[i].Break(scenarioData.targetValues[i], 0);
        }
    }

    public override bool IsFixed()
    {
        // No subsystem must be broken
        return subsystems.Count(x => !x.IsFixed()) == 0;
    }
}
