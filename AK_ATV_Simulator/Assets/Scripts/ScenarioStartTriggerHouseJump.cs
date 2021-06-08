using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioStartTriggerHouseJump : MonoBehaviour
{
    public GameObject endTrigger;
    public GameObject altEndTrigger;
    public string scenario;
    
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}


private void OnTriggerEnter(Collider other) {
        //Debug.Log("Trigger entered by " + other.gameObject.tag);
        if (other.gameObject.tag == "Player") {
            Debug.Log("Starting scenario for " + other.gameObject.tag);
            other.GetComponent<VehicleScenario>().Update_Scenario(scenario);
            endTrigger.SetActive(true);
            altEndTrigger.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}

