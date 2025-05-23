using UnityEngine;
using Random = UnityEngine.Random;

public class MissionGiver : MonoBehaviour
{
    private MissionButton[] _missionButtons;
    public GameObject missionButtons;
    private int _currentButtonIndex;

    private void OnEnable()
    {
        MissionButton.OnMissionAccepted += EndSignal;
    }

    private void OnDisable()
    {
        MissionButton.OnMissionAccepted -= EndSignal;
    }
    
    private void Awake()
    {
        _missionButtons = missionButtons.GetComponentsInChildren<MissionButton>();
    }

    public void StartSignal()
    {
        _currentButtonIndex = Random.Range(0, _missionButtons.Length);
        _missionButtons[_currentButtonIndex].SwitchLight();
    }

    private void EndSignal()
    {
        if (_currentButtonIndex != -1)
            _missionButtons[_currentButtonIndex].SwitchLight();
        _currentButtonIndex = -1;
    }
}
