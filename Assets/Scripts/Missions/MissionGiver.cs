using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionGiver : MonoBehaviour
{
    public List<ResourceObjective> possibleObjectives;
    public TextMeshPro text;
    private bool _isStarted;
    private int _progressionIndex;
    private ResourceObjective _currentObjective;
    private int _nbFail;
    
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
        }
        else
        {
            text.text = $"Mission failed... Press the button to get a new one.";
            _nbFail++;
        }
        _isStarted = false;
        _progressionIndex++;
        _currentObjective = null;
        
        if(_progressionIndex >= possibleObjectives.Count)
        {
            text.text = $"You completed all the missions, with a success rate of {(100f*_nbFail/possibleObjectives.Count):F2}%, thanks !.";
        }
    }

    private void FinishGame()
    {
        Debug.Log("FINISHED");
    }
    
}
