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
            throw new Exception($"ResourceSystem of CoordinatesScenarioData {name} must be a ResourceSystemCoordinates");
        }
        systemNetwork.Break(this);
    }
}
