using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passengerSceneEndTrigger : MonoBehaviour
{
    public GameObject startTrigger;
    public string endtext;
    public GameObject[] passengers;
    public GameObject mountainPassTrigger;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger entered by " + other.gameObject.name);
        if (other.gameObject.name == "VehicleCoords")
        {
            Debug.Log("Ending scenario for " + other.gameObject.name);
            other.GetComponent<VehicleScenario>().EndScenario(endtext);

            foreach (GameObject passenger in passengers)
            {
                passenger.SetActive(false);
            }
        }
    }
}

