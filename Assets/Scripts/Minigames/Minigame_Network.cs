using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Minigame_Network : ResourceSystem
{
    public List<CaseBehavior> casesList, casesSelected;
    public Color color1, color2, actualColor, standardColor;
    public bool isPathing;
    
    private CaseBehavior _lastCaseSelected, _lastColor1SelectedCase, _lastColor2SelectedCase;

    private void OnEnable()
    {
        isPathing = false;
        PlayerFocus.OnLoseFocus += OnLoseFocus;
        casesList = GetComponentsInChildren<CaseBehavior>().ToList();
    }

    public override void Break()
    {
        targetValue = 2;
        SetValue(0);
        Reset();
    }
    
    private void Reset()
    {
        foreach (var caseSelected in casesList)
        {
            caseSelected.GetComponent<SpriteRenderer>().color = caseSelected.baseColor;
            if (caseSelected != null && casesList.Contains(caseSelected) == false)
            {
                print(caseSelected);
                casesSelected.Remove(caseSelected);
            }
        }
        actualColor = standardColor;
        isPathing = false;
        casesSelected.Clear();
        SetValue(0);
    }

    private void ResetColor(Color color) //a continuer
    {
        foreach (var caseSelected in casesList)
        {
            if (caseSelected.baseColor == color)
            {
                caseSelected.GetComponent<SpriteRenderer>().color = caseSelected.baseColor;
                if (caseSelected != null && casesList.Contains(caseSelected) == false)
                {
                    print(caseSelected);
                    casesSelected.Remove(caseSelected);
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
            if (casesSelected.Contains(caseSelected))
            {
                Reset();
            }
            else if(caseSelected != _lastColor1SelectedCase && caseSelected != _lastColor2SelectedCase)
            {
                caseSelected.GetComponent<SpriteRenderer>().color = actualColor;
                casesSelected.Add(caseSelected);
                _lastCaseSelected = caseSelected;
            }
        }
        else if (caseSelected.endCase && caseSelected.baseColor == actualColor)
        {
            isPathing = false;
        }
    }

    public void CaseClicked(CaseBehavior caseSelected)
    {
        if (((caseSelected.baseColor == standardColor) || (caseSelected.endCase)) && (caseSelected != _lastColor1SelectedCase && caseSelected != _lastColor2SelectedCase))
        {
            Reset();
        }
        else
        {
            isPathing = true;
            if (caseSelected != _lastColor1SelectedCase && caseSelected != _lastColor2SelectedCase)
            {
                casesSelected.Add(caseSelected);
            }
            actualColor = caseSelected.GetComponent<SpriteRenderer>().color;
        }
    }

    public void CaseUnclicked(CaseBehavior caseSelected)
    {
        if (caseSelected.GetComponent<SpriteRenderer>().color == color1)
        {
            _lastColor1SelectedCase = _lastCaseSelected;
        }
        if (caseSelected.GetComponent<SpriteRenderer>().color == color2)
        {
            _lastColor2SelectedCase = _lastCaseSelected;
        }
    }
    
    private void OnLoseFocus()
    {
        foreach (var clickable in casesList)
        {
            clickable.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void ActivateChildren()
    {
        foreach (var clickable in casesList)
        {
            clickable.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
