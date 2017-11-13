using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {


    public GameObject Button;

    public GameObject pauseMenuCanvas;

    // Looks for Pause menu canvas and button.
    public void Start()
    {
        pauseMenuCanvas.SetActive(false);
        Button.SetActive(true);
    }


    // Pause canvas is set to true, button is set to false, and time scale is set to 0.
    public void OnPause()
    {
        Button.SetActive(false);
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    // Pause canvas is set to false, button is set to true, and time scale is set to normal.
    public void UnOnPause()
    {
        pauseMenuCanvas.SetActive(false);
        Button.SetActive(true);
        Time.timeScale = 1;
    }

    public void Resume()                      // Player can click on resume button to unpause the game.
    {
        pauseMenuCanvas.SetActive(false);
        Button.SetActive(true);
    }
    public void Quit()                   // Player can clck on quit button to shut down the game application. 
    {
        Application.Quit();
    }
}





