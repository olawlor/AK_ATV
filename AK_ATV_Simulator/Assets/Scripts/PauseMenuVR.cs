using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class PauseMenuVR : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public bool inOptionsMenu = false;
    public GameObject pauseMenuUI;
    public GameObject optionsUI;

    public SteamVR_Action_Boolean pauseAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "MenuClick");
    public SteamVR_Input_Sources pauseSource = SteamVR_Input_Sources.RightHand;
    private bool isPressed = false;

    void Start() {
        pauseAction.AddOnStateDownListener(ButtonPressed, pauseSource);
        pauseAction.AddOnStateUpListener(ButtonReleased, pauseSource);
    }
    // Update is called once per frame
    //void Update() {}

    public void ButtonPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        if (GameIsPaused && !isPressed && !inOptionsMenu) {
            Resume();
        }
        else if (!GameIsPaused && !isPressed && !inOptionsMenu) {
            Pause();
        }

        if (!isPressed) isPressed = true;
    }

    public void ButtonReleased(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        isPressed = false;
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadOptions() {
        inOptionsMenu = true;
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    public void BackButton() {
        optionsUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        inOptionsMenu = false;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu_VR");
    }

    public void QuitGame() {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
