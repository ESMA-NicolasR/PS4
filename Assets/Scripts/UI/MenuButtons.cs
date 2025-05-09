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
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
