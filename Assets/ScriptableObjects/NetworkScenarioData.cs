using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NetworkScenarioData", menuName = "Data/NetworkScenarioData")]
public class NetworkScenarioData : ResourceObjectiveData
{
    public GameObject boardPrefab;
    public int nbRows;
    public int nbColumns;
    
    public override void BreakSystem(ResourceSystem resourceSystem)
    {
        var systemNetwork = resourceSystem as ResourceSystemNetwork;
        if (systemNetwork == null)
        {
            throw new Exception($"ResourceSystem of NetworkScenarioData {name} must be a ResourceSystemNetwork");
        }
        systemNetwork.Break(this);
    }
    
    public override void End(ResourceSystem resourceSystem, bool isSuccess)
    {
        base.End(resourceSystem, isSuccess);
        var systemNetwork = resourceSystem as ResourceSystemNetwork;
        if (systemNetwork == null)
        {
            throw new Exception($"ResourceSystem of NetworkScenarioData {name} must be a ResourceSystemNetwork");
        }
        systemNetwork.minigameNetwork.CleanUp();
    }
}
