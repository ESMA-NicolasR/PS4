using UnityEngine;

public class CommonCase : MonoBehaviour
{
    private bool _selected;
    private string _color = "white";
    [SerializeField]
    private PathManager _pathManager;

    private void Update()
    {
        if (_selected == true && Input.GetButton("Fire1"))
        {
            _pathManager.CaseCrossed(_color, gameObject);
        }
    }

    void OnMouseEnter()
    {
        _selected = true;
    }

    void OnMouseExit()
    {
        _selected = false;
    }
}
