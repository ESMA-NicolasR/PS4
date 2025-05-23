using System;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursorOpen, _cursorClose, _cursorLeftRight, _cursorUpDown, _cursorCircle, _cursorFinger, _cursorEye;
    [SerializeField]
    private Vector2 _hotSpot;
    public static CursorManager Instance;

    private void Start()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        ChangeCursor(CursorType.Open);
    }

    public void ChangeCursor(CursorType cursorType)
    {
        switch (cursorType)
        {
            case CursorType.Open :
                Cursor.SetCursor(_cursorOpen, _hotSpot, CursorMode.Auto);
                break;
            case CursorType.Close :
                Cursor.SetCursor(_cursorClose, _hotSpot, CursorMode.Auto);
                break;
            case CursorType.LeftRight :
                Cursor.SetCursor(_cursorLeftRight, _hotSpot, CursorMode.Auto);
                break;
            case CursorType.UpDown :
                Cursor.SetCursor(_cursorUpDown, _hotSpot, CursorMode.Auto);
                break;
            case CursorType.Circle :
                Cursor.SetCursor(_cursorCircle, _hotSpot, CursorMode.Auto);
                break;
            case CursorType.Finger :
                Cursor.SetCursor(_cursorFinger, _hotSpot, CursorMode.Auto);
                break;
            case CursorType.Eye :
                Cursor.SetCursor(_cursorEye, _hotSpot, CursorMode.Auto);
                break;
        }
    }
}
