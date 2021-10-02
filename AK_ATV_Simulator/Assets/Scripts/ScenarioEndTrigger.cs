/*
  EndTrigger is used to end a scenario when you drive up to it.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioEndTrigger : MonoBehaviour
{
    public GameObject startTrigger;
    public string endtext;
    

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Trigger entered by " + other.gameObject.name);
        if (other.gameObject.name == "VehicleCoords") {
            other.GetComponent<VehicleScenario>().EndScenario(endtext);
            this.gameObject.SetActive(false);
        }
    }
}
