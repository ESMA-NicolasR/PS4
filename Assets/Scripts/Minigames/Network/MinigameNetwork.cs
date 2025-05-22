using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinigameNetwork : MiniGame<NetworkScenarioData>
{
    private List<Cell> _allCells = new();
    public bool _isPathing;
    private Cell _lastCellUsed, _lastCellUsedColor1, _lastCellUsedColor2, _cellEndColor1, _cellEndColor2;
    private List<Cell> _usedColor1Cells = new(), _usedColor2Cells = new();
    [SerializeField] private Sprite _spriteColor1Start, _spriteColor1End, _spriteColor1Finish, _spriteColor1Travel, _spriteColor1Ship;
    [SerializeField] private Sprite _spriteColor2Start, _spriteColor2End, _spriteColor2Finish, _spriteColor2Travel, _spriteColor2Ship;
    [SerializeField] private Sprite _spriteMeteor, _spriteNeutral;
    private GameObject _currentBoard;
    public Transform pivotBoard;

    public override void LaunchScenario()
    {
        base.LaunchScenario();
        _currentBoard = Instantiate(_scenario.boardPrefab, pivotBoard);
        // Prepare for interaction
        InitBoard(_scenario.nbRows, _scenario.nbColumns);
    }

    private void InitBoard(int rowNb, int columnNb)
    {
        _allCells = _currentBoard.GetComponentsInChildren<Cell>().ToList();
        var cellPosition = 0;
        // Init cells
        for (var i = 0; i < rowNb; i++)
        {
            for (var j = 0; j < columnNb; j++)
            {
                var cell = _allCells[cellPosition];
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
                cell.Init(j, i, this);
                cell.ChangeSprite(sprite);
                cellPosition +=1;
            }
        }
    }

    private void MoveFromTo(Cell cell1, Cell cell2)
    {
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
        // Change sprite of previous if neutral
        if (cell1.GetCellType() == CellType.Neutral)
        {
            switch (cell1.colorNb)
            {
                case 1:
                    cell1.ChangeSprite(_spriteColor1Travel);
                    break;
                case 2:
                    cell1.ChangeSprite(_spriteColor2Travel);
                    break;
            }
        }
        // Do things depending on type of next case
        switch (cell2.colorNb)
        {
            case 1:
                _lastCellUsedColor1 = cell2;
                _usedColor1Cells.Add(cell2);
                if (cell2.GetCellType() == CellType.Neutral)
                {
                    cell2.ChangeSprite(_spriteColor1Ship);
                }
                else if (cell2.GetCellType() == CellType.End)
                {
                    cell2.ChangeSprite(_spriteColor1Finish);
                    _isPathing = false;
                }
                break;
            case 2:
                _lastCellUsedColor2 = cell2;
                _usedColor2Cells.Add(cell2);
                if (cell2.GetCellType() == CellType.Neutral)
                {
                    cell2.ChangeSprite(_spriteColor2Ship);
                }
                else if (cell2.GetCellType() == CellType.End)
                {
                    cell2.ChangeSprite(_spriteColor2Finish);
                    _isPathing = false;
                }
                break;
        }
        _lastCellUsed = cell2;
    }

    public void StartPathFromCell(Cell cell)
    {
        switch (cell.GetCellType())
        {
            case CellType.Start:
                ResetColor(cell.colorNb);
                _isPathing = true;
                _lastCellUsed = cell;
                break;
            case CellType.Neutral:
                if (cell.isConnected && (cell == _lastCellUsedColor1 || cell == _lastCellUsedColor2))
                {
                    _isPathing = true;
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

    private void ActivateChildren(){
        foreach(Cell cell in _allCells){
            cell.Enable();
        }
    }

    private void DesactivateChildren()
    {
        foreach(Cell cell in _allCells){
            cell.Disable();
        }
    }

    public bool CheckIsWon()
    {
        // Minigame was never initialized
        if (_cellEndColor1 == null || _cellEndColor2 == null)
            return false;
        // Proper win condition
        return (_cellEndColor1.isConnected && _cellEndColor2.isConnected);
    }

    private void ResetColor(int colorNb)
    {
        switch (colorNb)
        {
            case 1:
                foreach (Cell cell in _usedColor1Cells)
                {
                    cell.ResetCell();
                    switch (cell.GetCellType())
                    {
                        case CellType.Neutral:
                            cell.ChangeSprite(_spriteNeutral);
                            break;
                        case CellType.End:
                            cell.ChangeSprite(_spriteColor1End);
                            break;
                    }
                }
                _usedColor1Cells.Clear();
                break;
            case 2:
                foreach (Cell cell in _usedColor2Cells)
                {
                    cell.ResetCell();
                    switch (cell.GetCellType())
                    {
                        case CellType.Neutral:
                            cell.ChangeSprite(_spriteNeutral);
                            break;
                        case CellType.End:
                            cell.ChangeSprite(_spriteColor2End);
                            break;
                    }
                }
                _usedColor2Cells.Clear();
                break;
        }
    }

    private void ResetAllColors()   
    {
        ResetColor(1);
        ResetColor(2);
    }
    
    public override void CleanUp()
    {
        base.CleanUp();
        // Clear lists
        _allCells.Clear();
        _usedColor1Cells.Clear();
        _usedColor2Cells.Clear();
        // Clear cell references
        _lastCellUsed = null;
        _lastCellUsedColor1 = null;
        _lastCellUsedColor2 = null;
        // Clear board
        if (_currentBoard != null)
        {
            Destroy(_currentBoard);
            _currentBoard = null;
        }
    }
}
