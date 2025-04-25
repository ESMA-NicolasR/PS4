using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionGiver : MonoBehaviour
{
    public List<ResourceObjective> possibleObjectives;
    public TextMeshPro text;
    private bool _isStarted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResourceObjective.OnObjectiveCompleted += OnObjectiveCompleted;
    }

    public void StartMission()
    {
        if (_isStarted) return;
        
        ResourceObjective objective = possibleObjectives[Random.Range(0, possibleObjectives.Count)];
        objective.CreateObjective();
        _isStarted = true;
        text.text = objective.GetDescription();
    }

    private void OnObjectiveCompleted()
    {
        text.text = $"Mission completed, congratulations ! Press the button to get a new one.";
        _isStarted = false;
    }
}
