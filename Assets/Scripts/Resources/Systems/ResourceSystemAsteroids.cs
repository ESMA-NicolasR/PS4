using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ResourceSystemAsteroids : ResourceSystem
{
    public MinigameAsteroids minigameAsteroids;

    public override void Break()
    {
        // TODO : generate random scenario
        base.Break();
    }

    public void Break(AsteroidScenarioData scenarioData)
    {
        SetValue(scenarioData.spawnList.Count);
        SetTargetValue(0);
        minigameAsteroids.PlayScenario(scenarioData);
    }
}
