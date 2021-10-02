/*
 For racing, this is an intermediate checkpoint.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioCheckpointTriggerTimer : MonoBehaviour
{
    public GameObject nextTrigger;

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Trigger entered by " + other.gameObject.tag);
        if (other.gameObject.tag == "Player") {
            other.GetComponent<VehicleScenario>().UpdateEnd(nextTrigger);
            this.gameObject.SetActive(false);
            nextTrigger.SetActive(true);
        }
    }
}
