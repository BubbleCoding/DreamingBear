using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script manages the main menu.

public class mainMenu : MonoBehaviour {

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame () // Start the game
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()  // Quit the game
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
