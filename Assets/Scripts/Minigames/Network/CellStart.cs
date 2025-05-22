public class CellStart : Cell
{
    private void Start()
    {
        isConnected = true;
    }

    public override CellType GetCellType()
    {
        return CellType.Start;
    }
    
    public override void ResetCell()
    {
        isConnected = false;
    }
}
