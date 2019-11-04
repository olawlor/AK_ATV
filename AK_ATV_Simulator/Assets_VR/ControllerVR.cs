/*
  This interfaces with the SteamVR input system,
  sending commands to the vehicle.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerVR : MonoBehaviour
{
    public VehicleProperties vehicle;
    public SteamVR_Action_Single throttleAction=SteamVR_Input.GetAction<SteamVR_Action_Single>("default", "Squeeze");
    public SteamVR_Input_Sources throttleSource=SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Single brakeAction=SteamVR_Input.GetAction<SteamVR_Action_Single>("default", "Squeeze");
    public SteamVR_Input_Sources brakeSource=SteamVR_Input_Sources.LeftHand;
    
    public SteamVR_Action_Pose framePoser=SteamVR_Input.GetAction<SteamVR_Action_Pose>("default", "Pose");
    public SteamVR_Input_Sources frameSource=SteamVR_Input_Sources.LeftHand;
    public SteamVR_Action_Pose steerPoser=SteamVR_Input.GetAction<SteamVR_Action_Pose>("default", "Pose");
    public SteamVR_Input_Sources steerSource=SteamVR_Input_Sources.RightHand;

    // Start is called before the first frame update
    void Start()
    {
        vehicle=GetComponent<VehicleProperties>();
    }
    
    public float throttle=0.0f;
    public float brake=0.0f;
    public Quaternion steerQuat;
    public float steer;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (vehicle) {
            // Apply keyboard motor and steering controls
            throttle=throttleAction.GetAxis(throttleSource);
            brake=brakeAction.GetAxis(brakeSource);
            vehicle.complementary_filter(0.1f,ref vehicle.cur_motor_power,throttle-brake);
            
            Vector3 framePos=framePoser.GetLocalPosition(frameSource);
            Quaternion frameRot=framePoser.GetLocalRotation(frameSource);
            
            Vector3 steerPos=steerPoser.GetLocalPosition(steerSource);
            Quaternion steerRot=steerPoser.GetLocalRotation(steerSource);
            steerQuat=steerRot*Quaternion.Inverse(frameRot);
            if (Mathf.Abs(steerQuat.w)>0.7f) { // reasonable steering configuration
                steer=steerQuat.y/steerQuat.w; // radians
                steer*=180.0f/Mathf.PI; // scale to degrees
                steer-=10.0f; // built-in rotation offset (steering trim)
                vehicle.complementary_filter(0.1f,ref vehicle.cur_steer,steer/25.0f);
            }
        }
    }
}
