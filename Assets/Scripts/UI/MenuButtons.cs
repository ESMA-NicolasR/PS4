using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuButtons : MonoBehaviour
{
    public GameObject creditsPanel;
    public string playScene;
    public string menuScene;
    public void Play()
    {
        SceneManager.LoadScene(playScene);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void ToggleCredits()
    {
        creditsPanel?.SetActive(!creditsPanel.activeSelf);
    }
}
