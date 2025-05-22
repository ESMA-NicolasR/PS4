using UnityEngine;
using UnityEngine.Serialization;

public class ResourceSystemNetwork : ResourceSystem
{
    public MinigameNetwork minigameNetwork;

    public void Break(NetworkScenarioData scenarioData)
    {
        SetValue(scenarioData.breakValue);
        SetTargetValue(scenarioData.targetValue);
        minigameNetwork.PlayScenario(scenarioData);
    }
    
    public override bool IsFixed()
    {
        return minigameNetwork.CheckIsWon();
    }
}
