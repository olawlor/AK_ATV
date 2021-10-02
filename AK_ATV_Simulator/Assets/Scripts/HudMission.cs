/*
 Display the current mission to the text message looking box on the HUD.
 
 Attach this script to the actual text box.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudMission : MonoBehaviour
{
    private TextMeshPro textmesh;
    
    // Start is called before the first frame update
    void Start()
    {
        textmesh=gameObject.GetComponent<TextMeshPro>();
    }

    private string last="";

    // Update is called once per frame
    void Update()
    {
        string msg = VehicleScenario.MissionGoal;
        if (msg!=null && msg != last)
        {
            textmesh.text = msg;
        }
        
        last=msg;
    }
}
