using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinigameNetwork : MonoBehaviour
{
    private List<Cell> _cells = new List<Cell>();
    private List<List<Cell>> _board = new List<List<Cell>>();
    private NetworkScenarioData _currentScenario;
    public bool _isPathing;
    private Cell _lastCellUsed, _lastCellUsedColor1, _lastCellUsedColor2, _cellEndColor1, _cellEndColor2;
    private List<Cell> _usedColor1Cells = new List<Cell>(), _usedColor2Cells = new List<Cell>();
    [SerializeField]
    private Sprite _spriteColor1Start, _spriteColor1End, _spriteColor1Travel, _spriteColor2Start, _spriteColor2End, _spriteColor2Travel, _spriteMeteor, _spriteNeutral;
    private int _columnNb = 0, _rowNb = 0;
    
    public void PlayScenario(NetworkScenarioData scenarioData)
    {
        _columnNb = 3; //change once we can initiate the board
        _rowNb = 4;
        InitBoard(_columnNb, _rowNb);
    }

    private void OnEnable()
    {
        foreach (Cell cell in GetComponentsInChildren<Cell>())
        {
            _cells.Add(cell);
            if (cell.GetCellType() == CellType.End)
            {
                switch (cell.colorNb)
                {
                    case 1 :
                        _cellEndColor1 = cell;
                        break;
                    case 2 :
                        _cellEndColor2 = cell;
                        break;
                }
            }
        }
    }

    private void InitBoard(int columnNb, int rowNb)
    {
        var cellPosition = 0;
        for (var i = 0; i < rowNb; i++)
        {
            _board.Add(new List<Cell>());
            for (var j = 0; j < columnNb; j++)
            {
                _cells[cellPosition].positionX = j;
                _cells[cellPosition].positionY = i;
                _board[i].Add(_cells[cellPosition]);
                switch (_cells[cellPosition].GetCellType())
                {
                    case CellType.Start:
                        switch (_cells[cellPosition].colorNb)
                        {
                            case 1:
                                _cells[cellPosition].ChangeSprite(_spriteColor1Start);
                                break;
                            case 2:
                                _cells[cellPosition].ChangeSprite(_spriteColor2Start);
                                break;
                        }
                        break;
                    case CellType.Neutral:
                        _cells[cellPosition].ChangeSprite(_spriteNeutral);
                        break;
                    case CellType.End:
                        switch (_cells[cellPosition].colorNb)
                        {
                            case 1:
                                _cells[cellPosition].ChangeSprite(_spriteColor1End);
                                break;
                            case 2:
                                _cells[cellPosition].ChangeSprite(_spriteColor2End);
                                break;
                        }
                        break;
                    case CellType.Meteor:
                        _cells[cellPosition].ChangeSprite(_spriteMeteor);
                        break;
                }
                cellPosition +=1;
            }
        }
    }

    private void MoveFromTo(Cell Cell1, Cell Cell2)
    {
        if ((Cell1.positionX - Cell2.positionX <= 1 && Cell1.positionX - Cell2.positionX >= -1 || Cell1.positionY - Cell2.positionY <= 1 && Cell1.positionY - Cell2.positionY >= -1) &&
            (Cell1.positionX - Cell2.positionX == 0 || Cell1.positionY - Cell2.positionY == 0)) //check if the cells are nearby
        {
            if (Cell2.CanMoveColor(Cell2.colorNb))
            {
                Cell2.ConnectColor(Cell1.colorNb);
                if (Cell2.GetCellType() == CellType.Neutral)
                {
                    switch (Cell2.colorNb)
                    {
                        case 1:
                            Cell2.ChangeSprite(_spriteColor1Travel);
                            break;
                        case 2:
                            Cell2.ChangeSprite(_spriteColor1Start);
                            break;
                    }
                }
                switch (Cell2.colorNb)
                {
                    case 1:
                        _lastCellUsedColor1 = Cell2;
                        _usedColor1Cells.Add(Cell2);
                        break;
                    case 2:
                        _lastCellUsedColor2 = Cell2;
                        _usedColor2Cells.Add(Cell2);
                        break;
                }
                _lastCellUsed = Cell2;
                if (CheckIsWon())
                {
                    Debug.Log("Won");
                }
            }
            else if (Cell2 != Cell1)
            {
                ResetColor(_lastCellUsed.colorNb);
            }
        }
    }

    public void StartPathFromCell(Cell Cell)
    {
        switch (Cell.GetCellType())
        {
            case CellType.Start:
                ResetColor(Cell.colorNb);
                _isPathing = true;
                _lastCellUsed = Cell;
                Cell.ConnectColor(Cell.colorNb);
                break;
            case CellType.Neutral:
                if (Cell == _lastCellUsedColor1 || Cell == _lastCellUsedColor2)
                {
                    _isPathing = true;
                    Cell.ConnectColor(Cell.colorNb);
                }
                else
                {
                    ResetColor(Cell.colorNb);
                }
                _lastCellUsed  = Cell;
                break;
            case CellType.Meteor:
                ResetAllColors();
                break;
        }
    }

    public void DrawPathOnCell(Cell Cell)
    {
        if (_isPathing)
        {
            if (Cell != _lastCellUsed)
            {
                MoveFromTo(_lastCellUsed, Cell);
            }
        }
    }

    public bool CheckIsWon()
    {
        return (_cellEndColor1.isConnected == true && _cellEndColor2.isConnected == true);
    }

    public void ResetColor(int ColorNb)
    {
        if (_cells.Count > 0)
        {
            foreach (Cell cell in _cells)
            {
                if (cell.colorNb == ColorNb)
                {
                    cell.ResetCell();
                }
            }
        }
    }

    public void ResetAllColors()   
    {
        foreach (Cell cell in _cells)
        {
            cell.ResetCell();
        }
    }
}
