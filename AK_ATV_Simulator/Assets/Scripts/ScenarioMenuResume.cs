using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMenuResume : MonoBehaviour
{
    public GameObject vehicle;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            VehicleScenario v=vehicle.GetComponent<VehicleScenario>();
            v.Resume();
        }
    }
}

