using UnityEngine;

public class ResourceSystemNetwork : ResourceSystem
{
    [SerializeField]
    private MinigameNetwork _minigameNetwork;

    public void Break(NetworkScenarioData scenarioData)
    {
        _minigameNetwork.PlayScenario(scenarioData);
    }
    
    public override bool IsFixed()
    {
        return _minigameNetwork.CheckIsWon();
    }
}
