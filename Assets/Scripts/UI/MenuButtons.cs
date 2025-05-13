using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuButtons : MonoBehaviour
{
    public string sceneToLoad;
    public void Play()
    {
        SceneManager.LoadScene(sceneToLoad);
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
