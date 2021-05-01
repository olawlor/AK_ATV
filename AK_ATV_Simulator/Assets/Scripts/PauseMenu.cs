/*! \file PauseMenu.cs
 * \brief The source for the class PauseMenu
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /*!< Flag for checking if the game is paused */
    public static bool GameIsPaused = false;
    /*!< Flag for if user is in the options menu */
    public bool inOptionsMenu = false;
    /*!< The object for the pause menu UI. This is assigned in Unity. */
    public GameObject pauseMenuUI;
    /*!< The object for the options UI. This is assigned in Unity. */
    public GameObject optionsUI; 

    /*! Update is called once per frame */
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            if(GameIsPaused && !inOptionsMenu){
                Resume();
            }
            else if(!inOptionsMenu){
                Pause();
            }
        }
    }

    /*! Returns user back to the game */ 
    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    
    /*! Sends user to the pause menu */
    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    /*! Makes the options in the options menu appear */
    public void LoadOptions(){
        inOptionsMenu = true;
        GameIsPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    /*! Allows the user to go back to the pause menu from the optios menu */
    public void BackButton(){
        optionsUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        inOptionsMenu = false;
    }

    /*! Loads the menu scene */
    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    /*! Allows users to quit the game */
    public void QuitGame(){
        Debug.Log("Quitting game");
        Application.Quit();
    }

    /*! Allows users to change the graphics quality settings */
     public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
