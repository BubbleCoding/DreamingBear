using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script manges the in game pause menu

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    void Update()   // check if we need to pause or resume the game
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() // Resume game
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Pause() // Pause game
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void LoadMenu()  // Go back to menu button
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()  // Quit game
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
