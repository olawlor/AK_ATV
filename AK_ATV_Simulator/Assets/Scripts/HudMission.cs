/*
 Display the current mission to the text message looking box on the HUD.
 
 Attach this script to the actual text element, and link to the background box.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudMission : MonoBehaviour
{
    public GameObject backgroundBox;
    private TextMeshPro textmesh;
    
    // Start is called before the first frame update
    void Start()
    {
        textmesh=gameObject.GetComponent<TextMeshPro>();
        VehicleScenario.UpdateMissionGoal("This phone will provide you with everything you need to know.");
        VehicleScenario.UpdateMissionGoal("The yellow triangle at the top of the compass points the way you are currently heading.");
        VehicleScenario.UpdateMissionGoal("when there is a pink line on the compass it will be pointing toward the objective");
        //need to figure out how to properly wait between messages
    }

    private string last="";

    // Update is called once per frame
    void Update()
    {
        // Compute mission elapsed time (MET)
        float MET = Time.time - VehicleScenario.MissionGoalTime;
        if (VehicleScenario.MissionEnd==null && MET>15.0f) {
            // Back to start mission text:
            VehicleScenario.UpdateMissionGoal("You can drive through another mission start arrow");
        }
        
        // Update color of background
        MeshRenderer box=backgroundBox.GetComponent<MeshRenderer>();
        if (MET>0.05f && box) {
            // Animate a color change:
            float r=0.9f;
            float g=0.2f;
            float b=0.4f;
            if (MET<2.0f) { r=1.0f; g=0.0f; b=0.0f; }
            box.material.color=new Color(r,g,b);
            
            // Animate a "bounce up" effect:
            float bounceUp=-0.1f*Mathf.Exp(-10.0f*MET);
            backgroundBox.transform.localPosition=new Vector3(0,0,bounceUp);
        }
        
        // Update our text box
        string msg = VehicleScenario.MissionGoal;
        if (msg!=null && msg != last)
        {
            textmesh.text = msg;
        }
        
        last=msg;
    }
}
