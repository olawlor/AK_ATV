/*
 Display the current vehicle speed in mph to the HUD text box.
 
 Attach this script to the actual text box, as a hierarchical child of the vehicle.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudMPH : MonoBehaviour
{
    private VehicleProperties vehicle;
    private TextMeshPro textmesh;
    
    // Start is called before the first frame update
    void Start()
    {
        vehicle=gameObject.GetComponentInParent<VehicleProperties>();
        textmesh=gameObject.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        int speed=Mathf.RoundToInt(vehicle.mph);
        if(speed <= 15 && speed >= -10){
            textmesh.color = Color.green;
        }else if((speed > 15 && speed <=20)||(speed <-10 && speed >=-15)){
            textmesh.color = Color.yellow;
        }else{
            textmesh.color = Color.red;
        }
        if (speed<0) speed=-speed;
        
        textmesh.text = speed + " mph";
    }
}
