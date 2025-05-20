using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public bool canPlay = false;
    public NetworkScenarioData networkScenarioData;
    public AsteroidScenarioData asteroidsScenarioData;
    public GameObject screenCheck;
    
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
        screenCheck.SetActive(true);
        networkScenarioData = networkScenarioData;
    }
    public void MiniGameAsteroidsCanStart(AsteroidScenarioData asteroidsScenarioData)
    {
        canPlay = true;
        screenCheck.SetActive(true);
        asteroidsScenarioData = asteroidsScenarioData;
    }

    public virtual void TurnOn(Focusable focusable)
    {
        if (focusable.GetComponentInParent<MiniGame>() != null && canPlay)
        {
            if (GetComponent<MinigameNetwork>() == true)
            {
                GetComponent<MinigameNetwork>().PlayScenario(networkScenarioData);
                screenCheck.SetActive(false);
                canPlay = false;
            }
            else if (GetComponent<MinigameAsteroids>() == true)
            {
                GetComponent<MinigameAsteroids>().PlayScenario(asteroidsScenarioData);
                GetComponent<MinigameAsteroids>().cursor.SetActive(true);
                screenCheck.SetActive(false);
                canPlay = false;
            }
        }
    }

    public virtual void TurnOff()
    {

    }
}
