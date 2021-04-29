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

    private void Start(){
        timing = true;
    }

    void Update() {
        if (timing)
            time += Time.deltaTime;
        else {
            Debug.Log("final time: " + time);
            time = 0.0f;
            timing = false;
        }

    }
    void DisplayTime(float timeToDisplay) {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;

        timeText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliSeconds);
    }
}