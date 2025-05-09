using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void EndTheGame()
    {
        SceneManager.LoadScene("EndingScene");
    }
}
