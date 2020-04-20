using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public bool inOptionsMenu = false;
    public GameObject pauseMenuUI;
    public GameObject optionsUI;
    // Update is called once per frame
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
    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    
    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadOptions(){
        inOptionsMenu = true;
        GameIsPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    public void BackButton(){
        optionsUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        inOptionsMenu = false;
    }

    public void LoadMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame(){
        Debug.Log("Quitting game");
        Application.Quit();
    }
     public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
