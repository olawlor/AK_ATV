using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passengerSceneStartTrigger : MonoBehaviour
{
    public GameObject endTrigger;
    public string scenario;
    public GameObject passenger;
    public GameObject secondPassenger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "VehicleCoords")
        {
            Debug.Log("Starting scenario for " + other.gameObject.tag);
            passenger.SetActive(true);
            
            other.GetComponent<VehicleScenario>().StartScenario(scenario,gameObject,endTrigger);
            
            secondPassenger.SetActive(true);
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            secondPassenger.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
