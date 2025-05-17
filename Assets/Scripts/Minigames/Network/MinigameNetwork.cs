using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinigameNetwork : MonoBehaviour
{
    private List<Cell> _cells;
    private List<List<Cell>> _board;
    private NetworkScenarioData _currentScenario;
    private bool _isPathing;
    private int _currentPathColor;
    private Cell _lastCellUsed, _cellEndColor1, _cellEndColor2;
    private List<Cell> _usedColor1Cells, _usedColor2Cells;
    [SerializeField]
    private Sprite _spriteColor1Start, _spriteColor1End, _spriteColor1Travel, _spriteColor2Start, _spriteColor2End, _spriteColor2Travel, _spriteMeteor, _spriteNeutral;
    private int _columnNb = 0, _rowNb = 0;
    
    public void PlayScenario(NetworkScenarioData scenarioData)
    {

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

    private void InitBoard()
    {
        _columnNb = 3; //change once we can initiate the board
        _rowNb = 4;
        var cellPosition = 0;
        for (var i = 0; i < _rowNb; i++)
        {
            _board.Add(new List<Cell>());
            for (var j = 0; j < _columnNb; j++)
            {
                _board[i].Add(_cells[cellPosition]);
                _cells[cellPosition].positionX = j;
                _cells[cellPosition].positionY = i;
                cellPosition +=1;
            }
        }
    }

    private void MoveFromTo(Cell Cell1, Cell Cell2)
    {
        
    }

    public void StartPathFromCell(Cell Cell)
    {
        
    }

    public void DrawPathOnCell(int ColorNb, Cell Cell)
    {
        
    }

    private bool CheckIsWon()
    {
        return (_cellEndColor1.isConnected == true && _cellEndColor2.isConnected == true);
    }

    private void ResetColor(int ColorNb)
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

    private void ResetAllColors()   
    {
        foreach (Cell cell in _cells)
        {
            cell.ResetCell();
        }
    }
}
