using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passengerSceneStartTrigger : MonoBehaviour
{
    public GameObject endTrigger;
    public string scenario;
    public GameObject[] passengers;
    public GameObject[] secondPassengers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "VehicleCoords")
        {
            Debug.Log("Starting scenario for " + other.gameObject.tag);
            foreach (GameObject passenger in passengers)
            {
                passenger.SetActive(true);
            }


            other.GetComponent<VehicleScenario>().StartScenario(scenario, gameObject, endTrigger);

            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            foreach (GameObject passenger in secondPassengers)
            {
                passenger.SetActive(true);
                passenger.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}
