using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CoordinatesScenarioData", menuName = "Data/CoordinatesScenarioData")]
public class CoordinatesScenarioData : ResourceObjectiveData
{
    public CoordinatesRecipient recipient;
    public int x, y, z;    
    public override void BreakSystem(ResourceSystem resourceSystem)
    {
        var systemNetwork = resourceSystem as ResourceSystemCoordinates;
        if (systemNetwork == null)
        {
            throw new Exception($"ResourceSystem of NetworkScenarioData {name} must be a ResourceSystemNetwork");
        }
        systemNetwork.Break(this);
    }
    
    public override void End(ResourceSystem resourceSystem)
    {
        base.End(resourceSystem);
        var systemNetwork = resourceSystem as ResourceSystemNetwork;
        if (systemNetwork == null)
        {
            throw new Exception($"ResourceSystem of NetworkScenarioData {name} must be a ResourceSystemNetwork");
        }
        systemNetwork.minigameNetwork.CleanUp();
    }
}
