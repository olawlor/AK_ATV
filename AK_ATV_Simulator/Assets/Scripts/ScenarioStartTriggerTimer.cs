﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioStartTriggerTimer : MonoBehaviour
{
    public GameObject checkpointTrigger;
    public GameObject timer;
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
            checkpointTrigger.SetActive(true);
            timer.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
