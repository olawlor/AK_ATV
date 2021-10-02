using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioEndTriggerTimer : MonoBehaviour
{
    public GameObject startTrigger;
    public GameObject resetRace;

    //public GameObject startTrigger;
    public string endtext;
    public GameObject timer;
    private Timer timerScript;

    // Start is called before the first frame update
    void Start()
    {
        timerScript=timer.GetComponent<Timer>();
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Trigger entered by " + other.gameObject.name);
        if (other.gameObject.name == "VehicleCoords") {
            
            string finalTime = timerScript.finalTime;
            timerScript.ResetTime();
            other.GetComponent<VehicleScenario>().EndScenario(endtext + finalTime);
            resetRace.SetActive(true);
            this.gameObject.SetActive(false);
            timer.SetActive(false);
        }
    }
}
