using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Minigame_Network : ResourceSystem
{
    private bool _isPaused;
    private List<CaseBehavior> _casesList;

    private void OnEnable()
    {
        PlayerFocus.OnLoseFocus += OnLoseFocus;
        _casesList = GetComponentsInChildren<CaseBehavior>().ToList();
    }
    
    private void OnLoseFocus()
    {
        _isPaused = true;
        foreach (var clickable in _casesList)
        {
            clickable.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void ActivateChildren()
    {
        _isPaused = false;
        foreach (var clickable in _casesList)
        {
            clickable.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
