using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passengerSceneStartTrigger : MonoBehaviour
{
    public GameObject endTrigger;
    public GameObject mountainPassTrigger;
    public string scenario;
    public GameObject passenger;
    public GameObject secondPassenger;
    bool doOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "VehicleCoords" && doOnce == false)
        {
            doOnce = true;
            Debug.Log("Starting scenario for " + other.gameObject.tag);
            endTrigger.SetActive(true);
            passenger.SetActive(true);
            mountainPassTrigger.SetActive(false);
            //StartCoroutine(StartingScenarios(other));
            other.GetComponent<VehicleScenario>().Update_Scenario(scenario);
                
            secondPassenger.SetActive(true);
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            secondPassenger.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
