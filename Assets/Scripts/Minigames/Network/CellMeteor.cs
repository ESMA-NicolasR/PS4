using UnityEngine;

public class CellMeteor : Cell
{
    public override bool CanMoveColor(int ColorNb)
    {
        return false;
    }

    public override CellType GetCellType()
    {
        return CellType.Meteor;
    }

    public override void OnMouseDown()
    {
        minigame.ResetAllColors();
    }
}
