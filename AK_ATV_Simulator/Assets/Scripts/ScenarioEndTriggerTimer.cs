using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioEndTriggerTimer : MonoBehaviour
{
    //public GameObject startTrigger;
    public string endtext;
    public GameObject timer;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Trigger entered by " + other.gameObject.name);
        if (other.gameObject.name == "VehicleCoords") {
            string finalTime = timer.GetComponent<Timer>().finalTime;
            Debug.Log("Ending scenario for " + other.gameObject.name);
            other.GetComponent<VehicleScenario>().Update_Scenario(endtext + finalTime);
            //startTrigger.SetActive(true);
            this.gameObject.SetActive(false);
            timer.SetActive(false);
        }
    }
}
