using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private static bool _isPaused;
    public static bool IsPaused => _isPaused;
    public GameObject pausePanel;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;
        pausePanel.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0 : 1;
    }
}
