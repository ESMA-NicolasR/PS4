using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinigameNetwork : MonoBehaviour
{
    private List<List<Cell>> _board;
    private NetworkScenarioData _currentScenario;
    private bool _isPathing;
    private int _currentPathColor;
    private Cell _lastCellUsed, _cellEndColor1, _cellEndColor2;
    private List<Cell> _usedColor1Cells, _usedColor2Cells;
    [SerializeField]
    private Sprite _spriteColor1Start, _spriteColor1End, _spriteColor1Travel, _spriteColor2Start, _spriteColor2End, _spriteColor2Travel, _spriteMeteor, _spriteNeutral;
    
    public void PlayScenario(NetworkScenarioData scenarioData)
    {

    }

    private void InitBoard()
    {
        
    }

    private void MoveFromTo(Cell Cell1, Cell Cell2)
    {
        
    }

    private void StartPathFromCell(Cell Cell)
    {
        
    }

    private void DrawPathOnCell(int ColorNb, Cell Cell)
    {
        
    }

    private bool CheckIsWon()
    {
        return true;
    }

    private void ResetColor(int ColorNb)
    {
        
    }

    private void ResetAllColors()   
    {
        
    }
}
