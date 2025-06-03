using UnityEngine;

public class Cell : MonoBehaviour
{
    public int positionX, positionY;
    public bool isConnected;
    public int colorNb; //0 = none, 1 =  color1, 2 = color2
    private MinigameNetwork _minigame;
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void Init(int _positionX, int _positionY, MinigameNetwork minigame)
    {
        positionX = _positionX;
        positionY = _positionY;
        _minigame = minigame;
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
        if (PauseMenu.IsPaused) return;
        _minigame.StartPathFromCell(this);
    }
    
    public void OnMouseEnter()
    {
        if (PauseMenu.IsPaused) return;
        if (Input.GetMouseButton(0))
        {
            _minigame.DrawPathOnCell(this);
        }
    }
    
    public void Disable()
    {
        _collider.enabled = false;
    }

    public void Enable()
    {
        _collider.enabled = true;
    }
}
