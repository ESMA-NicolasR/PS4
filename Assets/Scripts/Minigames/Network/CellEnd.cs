using System;
using UnityEngine;

public class CellEnd : Cell
{
    public override bool CanMoveColor(int ColorNb)
    {
        return ColorNb == colorNb;
    }

    public override CellType GetCellType()
    {
        return CellType.End;
    }

    public override void OnMouseDown()
    {
        
    }

    public void OnMouseUp()
    {
        Debug.Log("Feedback OnMouseUp");
    }
}
