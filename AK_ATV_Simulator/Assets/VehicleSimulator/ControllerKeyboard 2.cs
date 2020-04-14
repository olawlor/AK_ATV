using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerKeyboard : MonoBehaviour
{
    public VehicleProperties vehicle;
    
    // Start is called before the first frame update
    void Start()
    {
        vehicle=GetComponent<VehicleProperties>();
    }

    void FixedUpdate()
    {
        if (vehicle) {
            // Apply keyboard motor and steering controls
            float motor=0.0f;
            if (Input.GetKey ("w")) { motor=+1.0f; }
            if (Input.GetKey ("s")) { motor=-1.0f; }
            vehicle.complementary_filter(0.03f,ref vehicle.cur_motor_power,motor);
            
            float rotate=0.0f;
            if (Input.GetKey ("a")) { rotate=-1.0f; }
            if (Input.GetKey ("d")) { rotate=+1.0f; }
            vehicle.complementary_filter(0.01f,ref vehicle.cur_steer,rotate);
        }
    }
}
