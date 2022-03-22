using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioMenuResume : MonoBehaviour
{
    public GameObject[] vehicles;


    // Update is called once per frame
    void Update()
    {
        foreach (GameObject vehicle in vehicles)
        {
            if (vehicle.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // get whether gameobject is active or not from vehicle list
                    VehicleScenario v = vehicle.GetComponent<VehicleScenario>();
                    v.Resume();
                }
            }
        }

    }
}
