using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_VR : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("AK_ATV_VR_Simulator");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
}

