using UnityEngine;

public class ResourceSystemNetwork : ResourceSystem
{
    [SerializeField]
    private MinigameNetwork _minigameNetwork;

    public void Break(NetworkScenarioData scenarioData)
    {
        _minigameNetwork.MiniGameNetworkCanStart(scenarioData);
    }
}
