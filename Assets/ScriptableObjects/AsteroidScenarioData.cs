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
}
