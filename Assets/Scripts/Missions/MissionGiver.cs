using UnityEngine;
using Random = UnityEngine.Random;

public class MissionGiver : MonoBehaviour
{
    private MissionButton[] _missionButtons;
    public GameObject missionButtons;
    private int _currentButtonIndex;
    public List<ResourceObjectiveData> objectives;
    private Dictionary<SystemName, ResourceSystem> _namesToSystems;
    public TextMeshPro text;
    private bool _isStarted;
    private int _progressionIndex;
    private ResourceObjectiveData _currentObjective;
    private int _nbSuccess;
    private int _totalHumans, _totalMoney;

    private void OnEnable()
    {
        MissionButton.OnMissionAccepted += EndSignal;
    }

    private void OnDisable()
    {
        MissionButton.OnMissionAccepted -= EndSignal;
        if (_isStarted)
        {
            CheckObjectiveIsDone();
        }
        else if (_progressionIndex >= objectives.Count)
        {
            FinishGame();
        }
        else
        {
            StartMission();
        }
    }
    
    private void StartMission()
    {
        // Read the objective
        _currentObjective = objectives[_progressionIndex];
        // Break the system accordingly
        _currentObjective.BreakSystem(_namesToSystems[_currentObjective.systemName]);
        // Start the mission
        _isStarted = true;
        text.text = _currentObjective.description +" Push the button when it's done.";
        // Analytics
        AnalyticsObjectiveStarted?.Invoke();
    }
    
    private void CheckObjectiveIsDone()
    {
        bool isSuccess = _namesToSystems[_currentObjective.systemName].IsFixed();
        
        // Analytics
        AnalyticsObjectiveData data = new AnalyticsObjectiveData(_currentObjective.systemName.ToString(), isSuccess);
        AnalyticsObjectiveFinished?.Invoke(data);
        
        // Next objective
        if (isSuccess)
        {
            text.text = _currentObjective.winMessage;
            _nbSuccess++;
            Debug.Log($"Mission {_currentObjective.name} won");
            
            if (_currentObjective.humans > 0)
            {
                Debug.Log("money :" + _currentObjective.money);
                _totalMoney += _currentObjective.money;
            }
            else
            {
                Debug.Log("No money loss");
            }
            if (_currentObjective.humans > 0)
            {
                Debug.Log("humans :" + _currentObjective.humans);
                _totalHumans += _currentObjective.humans;
            }
            else
            {
                Debug.Log("No human loss");
            }
        }
        else //fail
        {
            text.text = _currentObjective.loseMessage;
            Debug.Log($"Mission {_currentObjective.name} failed");
            
            if (_currentObjective.money < 0)
            {
                Debug.Log("money :" + _currentObjective.money);
                _totalMoney += _currentObjective.money;
            }
            else
            {
                Debug.Log("No money won");
            }
            if (_currentObjective.humans < 0)
            {
                Debug.Log("humans :" + _currentObjective.humans);
                _totalHumans += _currentObjective.humans;
            }
            else
            {
                Debug.Log("No human won");
            }
        }
        _currentObjective.End(_namesToSystems[_currentObjective.systemName]);
        _isStarted = false;
        _progressionIndex++;
        _currentObjective = null;
        
        // Check ending
        if(_progressionIndex >= objectives.Count)
        {
            text.text = $"You completed all the missions, with a success rate of {(100f*_nbSuccess/objectives.Count):F2}%, thanks !";
        }
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
        AnalyticsManager.Instance.WriteAnalytics();
        Debug.Log("Game finished with humans :"+ _totalHumans + " money :" + _totalMoney);
        //SceneManager.LoadScene("EndingScene");
    }
}
