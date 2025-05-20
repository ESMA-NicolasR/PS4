using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public bool canPlay = false;
    public NetworkScenarioData _networkScenarioData;
    public AsteroidScenarioData _asteroidsScenarioData;
    public virtual void OnEnable()
    {
        PlayerFocus.OnLoseFocus += TurnOff;
        Focusable.OnGainFocus += TurnOn;
    }
    
    private void OnDisable()
    {
        PlayerFocus.OnLoseFocus += TurnOff;
        Focusable.OnGainFocus += TurnOn;
    }
    
    public void MiniGameNetworkCanStart(NetworkScenarioData networkScenarioData)
    {
        canPlay = true;
        _networkScenarioData = networkScenarioData;
    }
    public void MiniGameAsteroidsCanStart(AsteroidScenarioData asteroidsScenarioData)
    {
        canPlay = true;
        _asteroidsScenarioData = asteroidsScenarioData;
    }

    public virtual void TurnOn(Focusable focusable)
    {
        if (focusable.GetComponentInParent<MiniGame>() != null && canPlay)
        {
            if (GetComponent<MinigameNetwork>() == true)
            {
                GetComponent<MinigameNetwork>().PlayScenario(_networkScenarioData);
                canPlay = false;
            }
            else if (GetComponent<MinigameAsteroids>() == true)
            {
                GetComponent<MinigameAsteroids>().PlayScenario(_asteroidsScenarioData);
                GetComponent<MinigameAsteroids>().cursor.SetActive(true);
                canPlay = false;
            }
        }
    }

    public virtual void TurnOff()
    {

    }
}
