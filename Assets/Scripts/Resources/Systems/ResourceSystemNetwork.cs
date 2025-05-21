using UnityEngine;
using UnityEngine.Serialization;

public class ResourceSystemNetwork : ResourceSystem
{
    [FormerlySerializedAs("_minigameNetwork")] public MinigameNetwork minigameNetwork;

    public void Break(NetworkScenarioData scenarioData)
    {
        minigameNetwork.PlayScenario(scenarioData);
    }
}
