/*! \file MainMenu.cs
 * \brief The source for the class MainMenu
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
/**
 * using UnityEngine.SceneManagement is necessary for loading scenes
 */


//! The class for the WEBGL MainMenu 
public class MainMenu : MonoBehaviour
{
    /*! \fn public void StartGame()
     * \brief Runs when the game starts. Loads the procedural_terrain level
    */
    public void StartGame(){
        SceneManager.LoadScene("procedural_terrain");
    }

    /*! \fn public void ExitGame()
     * \brief Runs when the game ends. Outputs to log and exits the game.
    */
    public void ExitGame(){
        Debug.Log("Exiting");
        Application.Quit();
    }
}
