using UnityEngine;

public class Cell : MonoBehaviour
{
    public int positionX, positionY;
    public bool isConnected;
    public int colorNb; //0 = none, 1 =  color1, 2 = color2
    public MinigameNetwork minigame;

    private void Awake()
    {
        minigame = GetComponentInParent<MinigameNetwork>();
    }
    public void ConnectColor(int _colorNb)
    {
        colorNb = _colorNb;
        isConnected = true;
    }

    public virtual void ResetCell()
    {
        colorNb = 0;
        isConnected = false;
    }

    public void ChangeSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public virtual bool CanMoveColor(int _colorNb)
    {
        return !isConnected;
    }

    public virtual CellType GetCellType()
    {
        return CellType.Neutral;
    }

    public virtual void OnMouseDown()
    {
        minigame.StartPathFromCell(this);
    }
    
    public void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            minigame.DrawPathOnCell(this);
        }
    }
}
