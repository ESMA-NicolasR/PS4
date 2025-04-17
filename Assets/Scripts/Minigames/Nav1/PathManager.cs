using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public List<GameObject> _caseCrossedList;

    private GameObject _actualCase, _lastCase;
    private string _actualColor;
    private bool _isPathing = false;
    private int _winIndex = 0;

    void Start()
    {

    }

    public void SetStartingColor(string color, GameObject Case)
    {
        _actualColor = color;
        _actualCase = Case;
        _isPathing = true;
    }

    public void CaseCrossed(string color, GameObject Case)
    {
        if(Case != _lastCase)
        {
            if ((color == "white" && _isPathing == true) && _caseCrossedList.Contains(Case) == false)
            {
                if (_caseCrossedList.Contains(_lastCase) == false)
                {
                    _caseCrossedList.Add(_lastCase);
                }
                _lastCase = _actualCase;
                _actualCase = Case;
                if(_actualColor == "orange")
                {
                    Case.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0f);
                }
                if (_actualColor == "blue")
                {
                    Case.GetComponent<SpriteRenderer>().color = new Color(0f, 0.5f, 1f);
                }
            }
            else if (color == _actualColor && _isPathing == false)
            {
                _lastCase = _actualCase;
                _actualCase = Case;
                if (_actualColor == "orange")
                {
                    Case.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0f);
                }
                if (_actualColor == "blue")
                {
                    Case.GetComponent<SpriteRenderer>().color = new Color(0f, 0.5f, 1f);
                }
            }
            else
            {
                _isPathing = false;
                Reset();
            }
        }
    }

    public void EndIsReached(string color)
    {
        if(_actualColor == color && _isPathing == true)
        {
            if (_actualColor == "orange" && (_winIndex == 0 || _winIndex == 1))
            {
                _winIndex = 1;
            }
            else if (_actualColor == "blue" && (_winIndex == 0 || _winIndex == 2))
            {
                _winIndex = 2;
            }
            else print("stage cleared");
        }
    }

    public void Reset()
    {
        foreach (GameObject element in _caseCrossedList)
        {
            if (element != null && element.CompareTag("Case"))
            {
                element.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                if(_actualCase.CompareTag("Case"))
                {
                    _actualCase.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                }
            }
        }
        _actualColor = "";
        _caseCrossedList.Clear();
        _lastCase = null;
        _winIndex = 0;
    }
}
