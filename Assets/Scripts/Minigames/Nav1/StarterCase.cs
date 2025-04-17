using UnityEngine;

public class StarterCase : MonoBehaviour
{
    private bool _selected;
    [SerializeField]
    private string _color;
    [SerializeField]
    private PathManager _pathManager;

    private void Update()
    {
        if (_selected == true && Input.GetButton("Fire1"))
        {
            _pathManager.SetStartingColor(_color, gameObject);
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
