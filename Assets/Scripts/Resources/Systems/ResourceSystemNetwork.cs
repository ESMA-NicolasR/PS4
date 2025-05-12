using UnityEngine;

public class ResourceSystemNetwork : ResourceSystem
{
    [SerializeField]
    private Minigame_Network _minigameNetwork;

    public void Break(NetworkScenarioData scenarioData)
    {
        _minigameNetwork.PlayScenario(scenarioData);
    }
}
