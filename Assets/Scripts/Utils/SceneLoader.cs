using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Animator fadeScreenAnimator;
    public static SceneLoader Instance;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        // Cheap and dirty to unmask ui on load if necessary
        if (fadeScreenAnimator.GetComponent<Image>().color.a == 1)
        {
            fadeScreenAnimator.Play("FadeOut");
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithFade(sceneName));
    }

    IEnumerator LoadSceneWithFade(string sceneName)
    {
        // Fade to black and load the loading screen
        fadeScreenAnimator.Play("FadeIn");
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
