/*
  Trigger to start a scenario.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioStartTrigger : MonoBehaviour
{
    public GameObject endTrigger;
    public string scenario;

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Trigger entered by " + other.gameObject.tag);
        if (other.gameObject.tag == "Player") {
            other.GetComponent<VehicleScenario>().StartScenario(scenario,gameObject,endTrigger);
        }
    }
}
