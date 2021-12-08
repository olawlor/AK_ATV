/*! \file ControllerKeyboard.cs
* \brief The source for the class ControllerKeyboard
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerKeyboard : MonoBehaviour
{
    public VehicleProperties vehicle;
    /*! \fn  void Start()
    * \brief Start is called before the first frame update
    */
    void Start()
    {
        vehicle=GetComponent<VehicleProperties>();
    }

    /*! \fn  void FixedUpdate()
    * \brief Handles keyboard controls of the vehicle, and resets the vehicle when it is flipped
    */
    void FixedUpdate()
    {
        if (vehicle) {
            // Apply keyboard motor and steering controls
            if (!vehicle.is_VR) {
                float motor = 0.0f;
                if (Input.GetKey("w")||Input.GetKey("up")) { motor = +3.0f; }
                if (Input.GetKey("s")||Input.GetKey("down")) { motor = -3.0f; }
                vehicle.complementary_filter(1.5f*Time.fixedDeltaTime, ref vehicle.cur_motor_power, motor);
                
                if(!Input.GetKey("w") && !Input.GetKey("s") && !Input.GetKey("up") && !Input.GetKey("down")){vehicle.cur_brake_power=0.05f;}
                if (Input.GetKey("space")) { vehicle.cur_brake_power=0.5f;}
                
                float rotate = 0.0f;
                if (Input.GetKey("a")) { rotate = -1.0f; }
                if (Input.GetKey("d")) { rotate = +1.0f; }
                if (Input.GetKey("left")) { rotate = -1.0f; }
                if (Input.GetKey("right")) { rotate = +1.0f; }
                vehicle.complementary_filter(1.5f*Time.fixedDeltaTime, ref vehicle.cur_steer, rotate);
            }
            // Reset (after flip)
            if (Input.GetKey("r")) {
                vehicle.transform.position=vehicle.flat_Y(vehicle.transform.position);
                vehicle.transform.LookAt(vehicle.flat_Y(vehicle.transform.position+transform.forward*15.0f));
                vehicle.get_rb().velocity=Vector3.ClampMagnitude(vehicle.get_rb().velocity,5.0f); // limit linear velocity (don't zero it, for ice)
                vehicle.get_rb().angularVelocity=new Vector3(0.0f,0.0f,0.0f);
            }
        }
    }
}
