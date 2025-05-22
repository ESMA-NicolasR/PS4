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
        // Nothing should happen
    }

    public override void ResetCell()
    {
        isConnected = false;
    }
}
