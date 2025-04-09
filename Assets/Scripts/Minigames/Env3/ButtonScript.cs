using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    public bool selected;

    void OnMouseEnter()
    {
        selected = true;
        Debug.Log("Button was selected");
    }

    void OnMouseExit()
    {
        selected = false;
        Debug.Log("Button was deselected");
    }
}
