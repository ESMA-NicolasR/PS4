using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Minigame_Network : ResourceSystem
{
    public List<CaseBehavior> _casesList, _casesSelected;
    public Color color1, color2, actualColor;
    
    private CaseBehavior _lastSelectedCase, _selectedCase;

    private void OnEnable()
    {
        PlayerFocus.OnLoseFocus += OnLoseFocus;
        _casesList = GetComponentsInChildren<CaseBehavior>().ToList();
    }

    private void Reset()
    {
        print(_casesSelected);
        foreach (var caseSelected in _casesSelected)
        {
            if (caseSelected != null && _casesList.Contains(caseSelected) == false)
            {
                print(caseSelected);
                caseSelected.GetComponent<SpriteRenderer>().color = caseSelected.baseColor;
                _casesSelected.Remove(caseSelected);
            }
        }
    }
    
    public void CaseSelected(CaseBehavior caseSelected)
    {
        if (_casesSelected.Contains(caseSelected))
        {
            Reset();
        }
        else
        {
            _casesSelected.Add(caseSelected);
            caseSelected.GetComponent<SpriteRenderer>().color = actualColor;
            _selectedCase = caseSelected;
        }
    }

    public void CaseClicked(CaseBehavior caseSelected)
    {
        _casesSelected.Add(caseSelected);
        actualColor = caseSelected.baseColor;
        _selectedCase = caseSelected;
    }
    
    private void OnLoseFocus()
    {
        foreach (var clickable in _casesList)
        {
            clickable.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void ActivateChildren()
    {
        foreach (var clickable in _casesList)
        {
            clickable.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
