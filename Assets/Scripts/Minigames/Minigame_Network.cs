using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Minigame_Network : ResourceSystem
{
    public List<CaseBehavior> _casesList, _casesSelected;
    public Color color1, color2, actualColor, standardColor;
    public bool isPathing;
    
    private CaseBehavior _lastColor1SelectedCase, _lastColor2SelectedCase;

    private void OnEnable()
    {
        isPathing = false;
        PlayerFocus.OnLoseFocus += OnLoseFocus;
        _casesList = GetComponentsInChildren<CaseBehavior>().ToList();
    }

    public override void Break()
    {
        targetValue = 2;
        SetValue(0);
        Reset();
    }
    
    private void Reset()
    {
        foreach (var caseSelected in _casesList)
        {
            caseSelected.GetComponent<SpriteRenderer>().color = caseSelected.baseColor;
            if (caseSelected != null && _casesList.Contains(caseSelected) == false)
            {
                print(caseSelected);
                _casesSelected.Remove(caseSelected);
            }
        }
        actualColor = standardColor;
        isPathing = false;
        _casesSelected.Clear();
        SetValue(0);
    }

    private void ResetColor(Color color)
    {
        foreach (var caseSelected in _casesList)
        {
            if (caseSelected.baseColor == color)
            {
                caseSelected.GetComponent<SpriteRenderer>().color = caseSelected.baseColor;
                if (caseSelected != null && _casesList.Contains(caseSelected) == false)
                {
                    print(caseSelected);
                    _casesSelected.Remove(caseSelected);
                }
            }
        }
        actualColor = standardColor;
        isPathing = false;
        _lastColor2SelectedCase = null;
        _lastColor1SelectedCase = null;
        SetValue(0);
    }
    
    public void CaseSelected(CaseBehavior caseSelected)
    {
        if (isPathing && caseSelected.baseColor == standardColor)
        {
            if (_casesSelected.Contains(caseSelected))
            {
                Reset();
            }
            else
            {
                _casesSelected.Add(caseSelected);
                caseSelected.GetComponent<SpriteRenderer>().color = actualColor;
            }
        }
        else if (caseSelected.endCase && caseSelected.baseColor == actualColor)
        {
            isPathing = false;
            ChangeValue(1);
        }
    }

    public void CaseClicked(CaseBehavior caseSelected)
    {
        if (((caseSelected.baseColor == standardColor) || (caseSelected.endCase)) && (caseSelected != _lastColor1SelectedCase || caseSelected != _lastColor2SelectedCase))
        {
            Reset();
        }
        else
        {
            isPathing = true;
            if (caseSelected == _lastColor1SelectedCase || caseSelected == _lastColor2SelectedCase)
            {
                _casesSelected.Add(caseSelected);
            }
            actualColor = caseSelected.baseColor;
        }
    }

    public void CaseUnclicked(CaseBehavior caseSelected)
    {
        if (caseSelected.baseColor != standardColor)
        {
            if (caseSelected.baseColor == color1)
            {
                _lastColor1SelectedCase = caseSelected;
            }
            if (caseSelected.baseColor == color2)
            {
                _lastColor2SelectedCase = caseSelected;
            }
        }
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
