/*
  This interfaces with the SteamVR input system,
  sending commands to the vehicle.
  
  Add this script to the main vehicle.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerVR : MonoBehaviour
{
    // The main simulator
    private VehicleProperties vehicle;
    
    // Offset between VR play area and simulated ATV
    public GameObject VRAreaOffset;
    private bool setupVRArea=false;
    
    // Used for banking user's head as they make turns
    public GameObject BankingTurns;
    public float bank=0.0f;

    // VR copies of physical objects, that track with the user
    public GameObject rollcage;
    public GameObject handlebars;
    
    public SteamVR_Action_Single throttleAction=SteamVR_Input.GetAction<SteamVR_Action_Single>("default", "Squeeze");
    public SteamVR_Input_Sources throttleSource=SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Single brakeAction=SteamVR_Input.GetAction<SteamVR_Action_Single>("default", "Squeeze");
    public SteamVR_Input_Sources brakeSource=SteamVR_Input_Sources.LeftHand;
    
    public SteamVR_Action_Pose framePoser=SteamVR_Input.GetAction<SteamVR_Action_Pose>("default", "Pose");
    public SteamVR_Input_Sources frameSource=SteamVR_Input_Sources.LeftHand;
    public SteamVR_Action_Pose steerPoser=SteamVR_Input.GetAction<SteamVR_Action_Pose>("default", "Pose");
    public SteamVR_Input_Sources steerSource=SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Pose headPoser=SteamVR_Input.GetAction<SteamVR_Action_Pose>("default", "Pose");
    public SteamVR_Input_Sources headSource=SteamVR_Input_Sources.Head;

    // Start is called before the first frame update
    void Start()
    {
        vehicle=GetComponent<VehicleProperties>();
        vehicle.is_VR=true;
    }
    
    public float throttle=0.0f;
    public float brake=0.0f;
    public Quaternion steerQuat;
    public Vector3 steerAxis;
    public float steer; //<- handlebar rotation (only public for debugging)
    
    public Vector3 localHead;
    public Vector3 worldHead;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (vehicle) {
            // Apply keyboard motor and steering controls
            throttle=throttleAction.GetAxis(throttleSource);
            brake=brakeAction.GetAxis(brakeSource);
            vehicle.complementary_filter(0.1f,ref vehicle.cur_motor_power,throttle-brake);
            
            Vector3 headPos=headPoser.GetLocalPosition(headSource);
            localHead=headPos;
            Quaternion headRot=headPoser.GetLocalRotation(headSource);
            
            // Slightly bank the user's head during turns, 
            //   so the real gravity vector lines up with apparent acceleration.
            // Disadvantage: mismatch in user's roll gyros.
            vehicle.complementary_filter(0.1f,ref bank,vehicle.centripital);
            if (BankingTurns) { // Apply the turn
                // Reset full transform
                BankingTurns.transform.localRotation=Quaternion.Euler(0.0f,0.0f,0.0f);
                BankingTurns.transform.localPosition=new Vector3(0.0f,0.0f,0.0f);
                // Rotate around the user's head position
                worldHead=BankingTurns.transform.TransformPoint(headPos);
                Vector3 axis=BankingTurns.transform.forward; // vehicle.last_velocity;
                axis.y=0.0f;
                BankingTurns.transform.RotateAround(worldHead,axis,-30.0f*bank);
            }
            
            Vector3 framePos=framePoser.GetLocalPosition(frameSource);
            Quaternion frameRot=framePoser.GetLocalRotation(frameSource);
            
            if (VRAreaOffset && !setupVRArea && framePos.x!=0.0f) {
                // Locate VR camera relative to the physical frame:
                //   SteamVR does wacky things with the initial camera position,
                //   putting 0,0,0 as the center of the Steam VR room area.
                VRAreaOffset.transform.localPosition=new Vector3(-framePos.x,-framePos.y+1.05f-0.4f,-framePos.z+0.7f);
                setupVRArea=true;
            }
            
            // Plant rollcage relative to the physical frame
            rollcage.transform.localPosition=framePos+new Vector3(0.0f,-0.5f,-1.0f);
            rollcage.transform.localRotation=frameRot*Quaternion.Euler(-60.0f,180.0f,0.0f);
            handlebars.transform.localPosition=framePos+new Vector3(0.0f,-0.2f,-0.10f);
            
            Vector3 steerPos=steerPoser.GetLocalPosition(steerSource);
            Quaternion steerRot=steerPoser.GetLocalRotation(steerSource);
            steerQuat=steerRot*Quaternion.Inverse(frameRot);
            
            float angle = 0.0f;
            steerQuat.ToAngleAxis(out angle, out steerAxis);
            if (Mathf.Abs(steerAxis.y)>0.7f) { // we're in a reasonable steering configuration
                steer=angle;
                if (steerQuat.w>0.0f) steer=-angle;
                if (steer>180.0f) steer-=360.0f; // rotation is around zero
                steer=-steer;
                steer-=30.0f; // baked-in rotation offset (steering trim)
                vehicle.complementary_filter(0.1f,ref vehicle.cur_steer,steer/45.0f);
                handlebars.transform.localRotation=frameRot*Quaternion.Euler(-90.0f,180.0f,0.0f)*Quaternion.Euler(0.0f,steer,0.0f);
            }
        }
    }
}
