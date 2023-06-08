using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [Header("Canvases")]
    public GameObject menuCanvas;
    public GameObject settingsCanvas;
    public GameObject creditsCanvas;
    public GameObject pauseMenuCanvas; // will just remove this and place for now as temporary testing within scene for a while

    private AudioManager audioMgr;


    // Start is called before the first frame update
    void Start()
    {
        SetStartMenuCanvas();
        audioMgr = SingletonManager.Get<AudioManager>();
    }

    private void SetStartMenuCanvas()
    {
        menuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
    }
    

#region Menu Buttons
    public void OnPlaysButtonPressed()
    {
        // insert to go to the game itself
        Debug.Log("Going to Game!");
        
        SceneManager.LoadScene(1); // change inner parameter of this after changes
    }

    public void OnSettingsButtonPressed()
    {
        // show the settings button
        Debug.Log("Settings Canvas Active!");

        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
    }

    public void OnCreditsButtonPressed()
    {
        // go to the credits screen
        Debug.Log("Credits Canvas Active!");
        
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
    }

    public void OnPauseButtonPressed()
    {
        Debug.Log("Pause Button Active!");
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }

    public void OnBackButtonPressed()
    {
        // go back to the main menu screen
        Debug.Log("Back to Main Menu!");

        menuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
    }

    public void OnExitButtonPressed()
    {
        // quit the game
        Debug.Log("Quit Game!");

        Application.Quit(); 
    }

#endregion

}
