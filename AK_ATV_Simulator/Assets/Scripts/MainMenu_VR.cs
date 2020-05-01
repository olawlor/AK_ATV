using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_VR : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("procedural_terrain_VR");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
}

