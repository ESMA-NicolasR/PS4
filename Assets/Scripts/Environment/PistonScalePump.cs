using UnityEngine;

public class PistonScalePump : MonoBehaviour
{
    public Pump pump;
    public float minScale;
    void Update()
    {
        float yScale = Mathf.Lerp(minScale, 1, pump.progress);
        transform.localScale = new Vector3(1, yScale, 1);
    }
}
