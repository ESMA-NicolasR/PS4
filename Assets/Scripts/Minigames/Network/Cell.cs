using UnityEngine;

public class Cell : MonoBehaviour
{
    public int positionX, positionY;
    public bool isConnected;
    public int colorNb;
    public MinigameNetwork minigame;

    public void ConnectColor(int ColorNb)
    {
        
    }

    public void ResetCell()
    {
        
    }

    public void ChangeSprite(Sprite sprite)
    {
        
    }

    public bool CanMoveColor(int ColorNb)
    {
        return true;
    }

    public CellType GetCellType()
    {
        return CellType.Neutral;
    }

    private void OnMouseOn()
    {
        
    }
    
    private void OnMouseDrag()
    {
        
    }
}
