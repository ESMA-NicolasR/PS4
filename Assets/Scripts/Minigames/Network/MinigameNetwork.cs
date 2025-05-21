using System;
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
        var allCells = GetComponentsInChildren<Cell>().ToList();
        var cellPosition = 0;
        for (var i = 0; i < rowNb; i++)
        {
            _board.Add(new List<Cell>());
            for (var j = 0; j < columnNb; j++)
            {
                var cell = allCells[cellPosition];
                cell.positionX = j;
                cell.positionY = i;
                _board[i].Add(cell);
                Sprite sprite = _spriteNeutral;
                switch (cell.GetCellType())
                {
                    case CellType.Start:
                        switch (cell.colorNb)
                        {
                            case 1:
                                sprite = _spriteColor1Start;
                                break;
                            case 2:
                                sprite =  _spriteColor2Start;
                                break;
                        }
                        break;
                    case CellType.End:
                        switch (cell.colorNb)
                        {
                            case 1:
                                sprite = _spriteColor1End;
                                _cellEndColor1 = cell;
                                break;
                            case 2:
                                sprite = _spriteColor2End;
                                _cellEndColor2 = cell;
                                break;
                        }
                        break;
                    case CellType.Meteor:
                        sprite = _spriteMeteor;
                        break;
                    
                }
                cell.ChangeSprite(sprite);
                cellPosition +=1;
            }
        }
    }

    private void MoveFromTo(Cell cell1, Cell cell2)
    {
        Debug.Log(Math.Abs(cell1.positionX - cell2.positionX) == 1);
        Debug.Log(Math.Abs(cell1.positionY - cell2.positionY) == 1);
        Debug.Log(!(Math.Abs(cell1.positionX - cell2.positionX) != 0 ^ Math.Abs(cell1.positionY - cell2.positionY) == 1));
        // If it is more than 1 movement or diagonal movement, exit
        if (
            !(
                (Math.Abs(cell1.positionX - cell2.positionX) == 1 && cell1.positionY==cell2.positionY)
                ^ 
                (Math.Abs(cell1.positionY - cell2.positionY) == 1 && cell1.positionX==cell2.positionX)
            )
        )
        {
            _isPathing = false;
            return;
        }
        // If move is not allow, reset color and exit
        if (!cell2.CanMoveColor(cell1.colorNb))
        {
            _isPathing = false;
            ResetColor(cell1.colorNb);
            return;
        }
        // We know we can move
        cell2.ConnectColor(cell1.colorNb);
        if (cell2.GetCellType() == CellType.Neutral)
        {
            switch (cell1.colorNb)
            {
                case 1:
                    cell2.ChangeSprite(_spriteColor1Travel);
                    break;
                case 2:
                    cell2.ChangeSprite(_spriteColor2Travel);
                    break;
            }
        }
        switch (cell2.colorNb)
        {
            case 1:
                _lastCellUsedColor1 = cell2;
                _usedColor1Cells.Add(cell2);
                break;
            case 2:
                _lastCellUsedColor2 = cell2;
                _usedColor2Cells.Add(cell2);
                break;
        }
        _lastCellUsed = cell2;
        if (CheckIsWon())
        {
            Debug.Log("Won");
        }
    }

    public void StartPathFromCell(Cell cell)
    {
        switch (cell.GetCellType())
        {
            case CellType.Start:
                ResetColor(cell.colorNb);
                _isPathing = true;
                _lastCellUsed = cell;
                //cell.ConnectColor(cell.colorNb);
                break;
            case CellType.Neutral:
                if (cell.isConnected && (cell == _lastCellUsedColor1 || cell == _lastCellUsedColor2))
                {
                    _isPathing = true;
                    //cell.ConnectColor(cell.colorNb);
                }
                else
                {
                    _isPathing = false;
                    ResetColor(cell.colorNb);
                }
                _lastCellUsed  = cell;
                break;
            case CellType.Meteor:
                _isPathing = false;
                ResetAllColors();
                break;
        }
    }

    public void DrawPathOnCell(Cell cell)
    {
        if (_isPathing && cell != _lastCellUsed)
        {
            MoveFromTo(_lastCellUsed, cell);
        }
    }

    public bool CheckIsWon()
    {
        return (_cellEndColor1.isConnected && _cellEndColor2.isConnected);
    }

    public void ResetColor(int colorNb)
    {
        switch (colorNb)
        {
            case 1:
                foreach (Cell cell in _usedColor1Cells)
                {
                    cell.ResetCell();
                    if (cell.GetCellType() == CellType.Neutral)
                    {
                        cell.ChangeSprite(_spriteNeutral);
                    }
                }
                _usedColor1Cells.Clear();
                break;
            case 2:
                foreach (Cell cell in _usedColor2Cells)
                {
                    cell.ResetCell();
                    if (cell.GetCellType() == CellType.Neutral)
                    {
                        cell.ChangeSprite(_spriteNeutral);
                    }
                }
                _usedColor2Cells.Clear();
                break;
        }
    }

    public void ResetAllColors()   
    {
        ResetColor(1);
        ResetColor(2);
    }
}
