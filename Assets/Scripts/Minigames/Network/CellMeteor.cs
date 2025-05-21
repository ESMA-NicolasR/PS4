using UnityEngine;

public class CellMeteor : Cell
{
    public override bool CanMoveColor(int _colorNb)
    {
        return false;
    }

    public override CellType GetCellType()
    {
        return CellType.Meteor;
    }
}
