using UnityEngine;

public class Asteroid : MonoBehaviour
{
    void Start()
    {
        MinigameAsteroids.OnMoveCursor += OnMoveCursor;
    }
    private void OnMoveCursor(MinigameAsteroids minigameAsteroids)
    {
        print("heho");
    }
}
