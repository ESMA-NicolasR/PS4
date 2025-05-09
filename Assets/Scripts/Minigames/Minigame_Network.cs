using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Minigame_Network : ResourceSystem
{
    public List<CaseBehavior> casesList, casesSelectedColor1, casesSelectedColor2;
    public Color color1, color2, actualColor, standardColor;
    public bool isPathing;
    
    private CaseBehavior _lastCaseSelected, _lastColor1SelectedCase, _lastColor2SelectedCase;
    private Color _colorDone;

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
                casesSelectedColor1.Remove(caseSelected);
                casesSelectedColor2.Remove(caseSelected);
            }
        }
        actualColor = standardColor;
        isPathing = false;
        casesSelectedColor1.Clear();
        casesSelectedColor2.Clear();
        SetValue(0);
    }

    private void ResetColor(Color color) //a continuer
    {
        foreach (var caseSelected in casesList)
        {
            if (caseSelected.GetComponent<SpriteRenderer>().color == color)
            {
                caseSelected.GetComponent<SpriteRenderer>().color = caseSelected.baseColor;
                if (caseSelected != null && casesList.Contains(caseSelected) == false)
                {
                    print(caseSelected);
                    casesSelectedColor1.Remove(caseSelected);
                    casesSelectedColor2.Remove(caseSelected);
                }
            }
        }
        if (currentValue == 1)
        {
            if (_colorDone == color)
            {
                ChangeValue(-1);
            }
        }
        actualColor = standardColor;
        isPathing = false;
        _lastColor2SelectedCase = null;
        _lastColor1SelectedCase = null;
        if (color == color1)
        {
            _lastColor1SelectedCase = _lastCaseSelected;
            casesSelectedColor1.Clear();
        }
        if (color == color2)
        {
            _lastColor2SelectedCase = _lastCaseSelected;
            casesSelectedColor2.Clear();
        }
    }
    
    public void CaseSelected(CaseBehavior caseSelected)
    {
        if (isPathing && caseSelected.baseColor == standardColor)
        {
            if (casesSelectedColor1.Contains(caseSelected) || casesSelectedColor2.Contains(caseSelected))
            {
                Reset();
            }
            else
            {
                caseSelected.GetComponent<SpriteRenderer>().color = actualColor;
                if (caseSelected.GetComponent<SpriteRenderer>().color == color1)
                {
                    casesSelectedColor1.Add(caseSelected);
                }
                if (caseSelected.GetComponent<SpriteRenderer>().color == color2)
                {
                    casesSelectedColor2.Add(caseSelected);
                }
                _lastCaseSelected = caseSelected;
            }
        }
        else if (caseSelected.endCase && caseSelected.baseColor == actualColor)
        {
            isPathing = false;
            ChangeValue(1);
            _colorDone = actualColor;
        }
    }

    public void CaseClicked(CaseBehavior caseSelected)
    {
        if (caseSelected.GetComponent<SpriteRenderer>().color == standardColor && (caseSelected.endCase == false))
        {
            Reset();
        }
        if ((caseSelected != _lastColor1SelectedCase && caseSelected != _lastColor2SelectedCase) && (caseSelected.baseColor == standardColor && (caseSelected.endCase == false)))
        {
            ResetColor(caseSelected.GetComponent<SpriteRenderer>().color);
        }
        else
        {
            isPathing = true;
            if (casesList.Contains(caseSelected) == false)
            {
                if (caseSelected.GetComponent<SpriteRenderer>().color == color1)
                {
                    casesSelectedColor1.Add(caseSelected);
                }
                if (caseSelected.GetComponent<SpriteRenderer>().color == color2)
                {
                    casesSelectedColor2.Add(caseSelected);
                }
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

    public void Out()
    {
        isPathing = false;
    }
}
