using UnityEngine;

public class CellStart : Cell
{
    public override CellType GetCellType()
    {
        return CellType.Start;
    }
    
    public override void ResetCell()
    {
        isConnected = false;
    }
}
