using UnityEngine;

public class EndCase : MonoBehaviour
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
            _pathManager.EndIsReached(_color);
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
