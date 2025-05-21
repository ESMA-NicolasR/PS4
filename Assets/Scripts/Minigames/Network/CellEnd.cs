using System;
using UnityEngine;

public class CellEnd : Cell
{
    public override bool CanMoveColor(int _colorNb)
    {
        return _colorNb == colorNb;
    }

    public override CellType GetCellType()
    {
        return CellType.End;
    }

    public override void OnMouseDown()
    {
        
    }

    public override void ResetCell()
    {
        isConnected = false;
    }

    public void OnMouseUp()
    {
        Debug.Log("Feedback OnMouseUp");
    }
}
