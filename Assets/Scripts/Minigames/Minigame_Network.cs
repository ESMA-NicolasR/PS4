using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Minigame_Network : MonoBehaviour
{
    public List<CaseBehavior> casesList, casesSelectedColor1, casesSelectedColor2;
    public Color color1, color2, actualColor, standardColor;
    public bool isPathing;

    [SerializeField]
    private Transform _pivotBoard;
    [SerializeField]
    private ResourceSystemNetwork _resourceSystemNetwork;
    private CaseBehavior _lastCaseSelected, _lastColor1SelectedCase, _lastColor2SelectedCase;
    private Color _colorDone;

    private void OnEnable()
    {
        isPathing = false;
        PlayerFocus.OnLoseFocus += OnLoseFocus;
    }
    
    public void PlayScenario(NetworkScenarioData scenarioData)
    {
        Instantiate(scenarioData.boardPrefab, _pivotBoard);
        casesList = GetComponentsInChildren<CaseBehavior>().ToList();
        _resourceSystemNetwork.targetValue = 2;
        _resourceSystemNetwork.SetValue(0);
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
        _resourceSystemNetwork.SetValue(0);
    }

    private void ResetColor(Color color)
    {
        foreach (var caseSelected in casesList)
        {
            if (caseSelected.GetComponent<SpriteRenderer>().color == color)
            {
                caseSelected.GetComponent<SpriteRenderer>().color = caseSelected.baseColor;
                if (caseSelected != null && casesList.Contains(caseSelected) == false)
                {
                    casesSelectedColor1.Remove(caseSelected);
                    casesSelectedColor2.Remove(caseSelected);
                }
            }
        }
        if (_resourceSystemNetwork.currentValue == 1)
        {
            if (_colorDone == color)
            {
                _resourceSystemNetwork.ChangeValue(-1);
            }
        }
        else if (_resourceSystemNetwork.currentValue == 2)
        {
            _resourceSystemNetwork.ChangeValue(-1);
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
        else if (isPathing && caseSelected.endCase)
        {
            isPathing = false;
            _resourceSystemNetwork.ChangeValue(1);
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
        else if (caseSelected.endCase || ((casesSelectedColor1.Any() && caseSelected.baseColor == color1) || (casesSelectedColor2.Any() && caseSelected.baseColor == color2)))
        {
            isPathing = false;
        }
        else if(caseSelected.endCase == false)
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
