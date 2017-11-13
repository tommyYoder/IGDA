using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public void Start()
    {
        instance = this;
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadLevelAsync(string levelName)
    {
        StartCoroutine(AsynchronousLevelLoad(levelName));
    }

    IEnumerator AsynchronousLevelLoad(string levelName)
    {
        AsyncOperation AsynLoadOperation = SceneManager.LoadSceneAsync(levelName);

        yield return null;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
