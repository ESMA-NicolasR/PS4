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
        var minigameAsteroids = resourceSystem as ResourceSystemAsteroids;
        if (minigameAsteroids == null)
        {
            throw new Exception($"ResourceSystem of AsteroidScenarioData {name} must be a MinigameAsteroids");
        }
        minigameAsteroids.Break(this);
    }
}
