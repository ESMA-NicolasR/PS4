using System;
using UnityEngine;

public class ResourceSystemAsteroids : ResourceSystem
{
    [SerializeField]
    private MinigameAsteroids _minigameAsteroids;

    public override void Break()
    {
        // TODO : generate random scenario
        base.Break();
    }

    public void Break(AsteroidScenarioData scenarioData)
    {
        _minigameAsteroids.MiniGameAsteroidsCanStart(scenarioData);
    }
}
