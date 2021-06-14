/*! \file Timer.cs
* \brief The source for the class Timer
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {
    public float time = 0.0f;
    public bool timing = false;
    public Text timeText;
    public string finalTime = "";

    private void Start(){
        timing = true;
    }

    void Update() {
        if (timing){
            time += 1.6f * Time.deltaTime;
            DisplayTime(time);
        }
        else {
            Debug.Log("final time: " + time);
            timing = false;
            time = 0.0f;
        }

    }
    void ResetTime(){
        time = 0.0f;
    }
    void DisplayTime(float timeToDisplay) {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;

        timeText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliSeconds);
        finalTime = timeText.text;
    }
}