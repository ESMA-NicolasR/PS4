using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinigameNetwork : MiniGame<NetworkScenarioData>
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
    private GameObject _currentBoard;

    public override void LaunchScenario()
    {
        base.LaunchScenario();
        _currentBoard = Instantiate(_scenario.boardPrefab, _pivotBoard);
        // Init cases
        casesList = GetComponentsInChildren<CaseBehavior>().ToList();
        foreach (CaseBehavior caseBehavior in casesList)
        {
            caseBehavior.minigameNetwork = this;
        }
        foreach (MeteorCaseBehavior meteor in GetComponentsInChildren<MeteorCaseBehavior>())
        {
            meteor.minigameNetwork = this;
        }
        // Set system values
        _resourceSystemNetwork.targetValue = 2;
        _resourceSystemNetwork.SetValue(0);
        // Prepare for interaction
        Reset();
    }
    
    
    private void Reset()
    {
        foreach (var caseSelected in casesList)
        {
            caseSelected.ChangeColor(caseSelected.baseColor);
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
            if (caseSelected.IsColor(color))
            {
                caseSelected.ChangeColor(caseSelected.baseColor);
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
                caseSelected.ChangeColor(actualColor);
                if (caseSelected.IsColor(color1))
                {
                    casesSelectedColor1.Add(caseSelected);
                }
                if (caseSelected.IsColor(color2))
                {
                    casesSelectedColor2.Add(caseSelected);
                }
                _lastCaseSelected = caseSelected;
            }
        }
        else if (isPathing && (caseSelected.endCase && caseSelected.baseColor == actualColor))
        {
            isPathing = false;
            _resourceSystemNetwork.ChangeValue(1);
            _colorDone = actualColor;
        }
        else if (isPathing && caseSelected.startCase)
        {
            print("heho");
            ResetColor(actualColor);
        }
    }

    public void CaseClicked(CaseBehavior caseSelected)
    {
        if (caseSelected.IsColor(standardColor) && caseSelected.endCase == false)
        {
            Reset();
        }
        if ((caseSelected != _lastColor1SelectedCase && caseSelected != _lastColor2SelectedCase) && (caseSelected.baseColor == standardColor && (caseSelected.endCase == false)))
        {
            ResetColor(caseSelected.GetColor());
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
                if (caseSelected.IsColor(color1))
                {
                    casesSelectedColor1.Add(caseSelected);
                }
                if (caseSelected.IsColor(color2))
                {
                    casesSelectedColor2.Add(caseSelected);
                }
            }
            actualColor = caseSelected.GetColor();
        }
    }

    public void CaseUnclicked(CaseBehavior caseSelected)
    {
        if (caseSelected.IsColor(color1))
        {
            _lastColor1SelectedCase = _lastCaseSelected;
        }
        if (caseSelected.IsColor(color2))
        {
            _lastColor2SelectedCase = _lastCaseSelected;
        }
    }
    
    public override void GainFocus()
    {
        base.GainFocus();
        ActivateChildren();
    }

    public override void LoseFocus()
    {
        base.LoseFocus();
        DesactivateChildren();
    }
    private void DesactivateChildren()
    {
        foreach (CaseBehavior caseBehaviour in casesList)
        {
            caseBehaviour.Disable();
        }
    }

    public void ActivateChildren()
    {
        foreach (CaseBehavior caseBehaviour in casesList)
        {
            caseBehaviour.Enable();
        }
    }

    public void Out()
    {
        isPathing = false;
    }
    
    public override void CleanUp()
    {
        base.CleanUp();
        casesList = new List<CaseBehavior>();
        if (_currentBoard != null)
        {
            Destroy(_currentBoard);
            _currentBoard = null;
        }
    }
}
