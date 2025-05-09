using UnityEngine;

public class Outlined : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.materials[1].SetVector("_Center", meshRenderer.bounds.center);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
