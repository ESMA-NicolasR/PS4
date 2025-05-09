using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionGiver : MonoBehaviour
{
    public List<IObjective> possibleObjectives;
    public TextMeshPro text;
    private bool _isStarted;
    private int _progressionIndex;
    private IObjective _currentObjective;
    private int _nbSuccess;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isStarted = false;
        _progressionIndex = 0;
    }

    public void StartMission()
    {
        if (_isStarted)
        {
            CheckObjectiveIsDone();
        }
        else if (_progressionIndex >= possibleObjectives.Count)
        {
            FinishGame();
        }
        else
        {
            StartObjective();
        }
    }

    private void StartObjective()
    {
        _currentObjective = possibleObjectives[_progressionIndex];
        _currentObjective.CreateObjective();
        _isStarted = true;
        text.text = _currentObjective.GetDescription() +". Push the button when it's done.";
    }
    
    private void CheckObjectiveIsDone()
    {
        if (_currentObjective.CheckIsCompleted())
        {

            text.text = $"Mission completed, congratulations ! Press the button to get a new one.";
            _nbSuccess++;
        }
        else
        {
            text.text = $"Mission failed... Press the button to get a new one.";
        }
        _isStarted = false;
        _progressionIndex++;
        _currentObjective = null;
        
        if(_progressionIndex >= possibleObjectives.Count)
        {
            text.text = $"You completed all the missions, with a success rate of {(100f*_nbSuccess/possibleObjectives.Count):F2}%, thanks !";
        }
    }

    private void FinishGame()
    {
        SceneManager.LoadScene("EndingScene");
    }
    
}
