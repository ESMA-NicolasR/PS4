using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuButtons : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("AlphaLevel");
    }

    public void Quit()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
