using UnityEngine;
using UnityEngine.Splines;

public class Station : MonoBehaviour
{
    public TravelPath leftPath, rightPath, backPath;

    public TravelPath GetPath(TravelDirection direction)
    {
        switch (direction)
        {
            case TravelDirection.Left:
                return leftPath;
            case TravelDirection.Right:
                return rightPath;
            case TravelDirection.Back:
                return backPath;
            default:
                return null;
        }
    }
}
