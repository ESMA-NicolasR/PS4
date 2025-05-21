using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AsteroidSpawnData
{
    public float spawnDelay;
    public float moveSpeed;
    public float growSpeed;
    public float scale;
    public Vector2 direction;
    public Vector2 position;
}

[CreateAssetMenu(fileName = "AsteroidScenarioData", menuName = "Data/AsteroidScenarioData")]
public class AsteroidScenarioData : ResourceObjectiveData
{
    public List<AsteroidSpawnData> spawnList;

    public override void BreakSystem(ResourceSystem resourceSystem)
    {
        var systemAsteroids = resourceSystem as ResourceSystemAsteroids;
        if (systemAsteroids == null)
        {
            throw new Exception($"ResourceSystem of AsteroidScenarioData {name} must be a MinigameAsteroids");
        }
        systemAsteroids.Break(this);
    }

    public override void End(ResourceSystem resourceSystem)
    {
        base.End(resourceSystem);
        var systemAsteroids = resourceSystem as ResourceSystemAsteroids;
        if (systemAsteroids == null)
        {
            throw new Exception($"ResourceSystem of AsteroidScenarioData {name} must be a MinigameAsteroids");
        }
        systemAsteroids.minigameAsteroids.CleanUp();
    }
}
