using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [Header("Canvases")]
    public GameObject tapToContinuePanel;
    public GameObject menuCanvas;
    public GameObject scoreboardCanvas;
    public GameObject settingsCanvas;
    public GameObject creditsCanvas;
    public GameObject pauseMenuCanvas; // will just remove this and place for now as temporary testing within scene for a while
    public GameObject quitCanvas;

    [Header("Quit Panel")]
    public Button tapToContButton;

    private AudioManager audioMgr;

    // Start is called before the first frame update
    void Start()
    {
        audioMgr = SingletonManager.Get<AudioManager>();
        SetStartMenuCanvas();
    }

    void Update()
    {
        if (Input.anyKeyDown && tapToContinuePanel.activeSelf)
        {
            Debug.Log("Proceed to Menu Panel");
            OnTapToContPressed();                
        }
    }

    private void SetStartMenuCanvas()
    {
        tapToContinuePanel.SetActive(true);
        scoreboardCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        quitCanvas.SetActive(false);
    }
    

#region Menu Buttons

    public void OnTapToContPressed()
    {
        // show the settings button
        Debug.Log("Menu Canvas Active!");

        audioMgr.PlaySFX("Button Click 2");

        tapToContinuePanel.SetActive(false);
        scoreboardCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        quitCanvas.SetActive(false);
    }

    public void OnPlaysButtonPressed()
    {
        // insert to go to the game itself
        Debug.Log("Going to Game!");
        
        SceneManager.LoadScene(1); // change inner parameter of this after changes
    }

    public void OnControlsButtonPressed()
    {
        Debug.Log("Controls Panel Active!");
    }

    public void OnScoreboardButtonPressed()
    {
        Debug.Log("Scoreboard Panel Active!");

        tapToContinuePanel.SetActive(false);
        scoreboardCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        quitCanvas.SetActive(false);
    }

    public void OnSettingsButtonPressed()
    {
        // show the settings button
        Debug.Log("Settings Canvas Active!");

        tapToContinuePanel.SetActive(false);
        scoreboardCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        quitCanvas.SetActive(false);
    }

    public void OnCreditsButtonPressed()
    {
        // go to the credits screen
        Debug.Log("Credits Canvas Active!");
        
        tapToContinuePanel.SetActive(false);
        scoreboardCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
        quitCanvas.SetActive(false);
    }

    public void OnPauseButtonPressed()
    {
        Debug.Log("Pause Button Active!");

        tapToContinuePanel.SetActive(false);
        scoreboardCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
        quitCanvas.SetActive(false);
    }

    public void OnBackButtonPressed()
    {
        // go back to the main menu screen
        Debug.Log("Back to Main Menu!");

        tapToContinuePanel.SetActive(false);
        scoreboardCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        quitCanvas.SetActive(false);
    }

    public void OnExitButtonPressed()
    {
        // quit the game
        Debug.Log("Show Quit Prompt!");

        tapToContinuePanel.SetActive(false);
        scoreboardCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        quitCanvas.SetActive(true);
    }

    public void OnQuitOptionsSelected(bool quitOption)
    {
        if (quitOption == true)
        {
            Debug.Log("Quit Game!");
            Application.Quit();
        }
        else
        {
            OnBackButtonPressed();
        }
    }

#endregion

}
