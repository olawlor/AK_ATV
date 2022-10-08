/*! \file PauseMenu.cs
 * \brief The source for the class PauseMenu
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{


    /*!< Flag for checking if the game is paused */
    public static bool GameIsPaused = false;
    /*!< Flag for if user is in the options menu */
    public bool inOptionsMenu = false;
    /* Flag for when user is in vehicles menu */
    public bool inVehiclesMenu = false;
    /* Flag for when user is in controls menu */
    public bool inControlsMenu = false;
    /* Flag for if user is using mobile controls */
    public bool isMobile = false;

    /*!< The object for the pause menu UI. This is assigned in Unity. */
    public GameObject mobileUI;
    /*!< The object for the pause menu UI. This is assigned in Unity. */
    public GameObject pauseMenuUI;
    /*!< The object for the options UI. This is assigned in Unity. */
    public GameObject optionsUI;
    /* The object for the vehicles UI. This is assigned in Unity. */
    public GameObject vehiclesUI;
    /* The objects for the controls UI. This is assigned in Unity. */
    public GameObject controlsUI;

    public VehicleScenario scenarios1;
    public VehicleScenario scenarios2;
    public VehicleScenario scenarios3;

    /*! Update is called once per frame */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if ((GameIsPaused && !inOptionsMenu) &&
                (GameIsPaused && !inVehiclesMenu) &&
                (GameIsPaused && !inControlsMenu))
            {
                Resume();
            }
            else if (!inOptionsMenu && !inVehiclesMenu && !inControlsMenu)
            {
                Pause();
            }
        }
    }

    /*! Returns user back to the game */
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        if (isMobile)
            mobileUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    /*! Sends user to the pause menu */
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        if (isMobile)
            mobileUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    /*! Makes the options in the options menu appear */
    public void LoadOptions()
    {
        inOptionsMenu = true;
        GameIsPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(true);
        vehiclesUI.SetActive(false);
        controlsUI.SetActive(false);
    }

    /* Loads vehicle selection menu
     * If you are in a scenario, vehicle switching will be locked. 
     */
    public void LoadVehicles()
    {

        if (!scenarios1.inScenario && !scenarios2.inScenario && !scenarios3.inScenario)
        {
            inVehiclesMenu = true;
            GameIsPaused = true;
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(false);
            vehiclesUI.SetActive(true);
            optionsUI.SetActive(false);
            controlsUI.SetActive(false);
        }
        else
        {
            Debug.Log("ERROR: Cannot switch vehicles while in a scenario!");
        }
    }
    /*! Loads menu for controls */
    public void LoadControls()
    {
        inControlsMenu = true;
        GameIsPaused = true;
        Time.timeScale = 0f;
        controlsUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(false);
        vehiclesUI.SetActive(false);
    }

    /*! Allows the user to go back to the pause menu from the options menu */
    public void BackButton()
    {
        controlsUI.SetActive(false);
        optionsUI.SetActive(false);
        vehiclesUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        inOptionsMenu = false;
        inVehiclesMenu = false;
        inControlsMenu = false;
    }

    /*! Prompts tutorial messages to be sent to phone gui */
    public void Tutorial()
    {
        optionsUI.SetActive(false);
        vehiclesUI.SetActive(false);
        controlsUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        //add the start tutorial cmd here
    }

    /*! Loads the menu scene */
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    /*! Allows users to quit the game */
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    /*! Allows users to change the graphics quality settings */
    public void SetQuality(int qualityIndex)
    {
        Debug.Log("Setting graphics quality level to "+qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }

}

