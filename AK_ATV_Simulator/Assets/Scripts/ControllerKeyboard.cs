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
                if (Input.GetKey("w")) { motor = +1.0f; }
                if (Input.GetKey("s")) { motor = -1.0f; }
                vehicle.complementary_filter(0.03f, ref vehicle.cur_motor_power, motor);

                float rotate = 0.0f;
                if (Input.GetKey("a")) { rotate = -1.0f; }
                if (Input.GetKey("d")) { rotate = +1.0f; }
                vehicle.complementary_filter(0.01f, ref vehicle.cur_steer, rotate);
            }
            // Reset (after flip)
            if (Input.GetKey("r")) {
                vehicle.transform.position=vehicle.flat_Y(vehicle.transform.position);
                vehicle.transform.LookAt(vehicle.flat_Y(vehicle.transform.position+transform.forward*10.0f));
                vehicle.get_rb().velocity=Vector3.ClampMagnitude(vehicle.get_rb().velocity,5.0f); // limit linear velocity (don't zero it, for ice)
                vehicle.get_rb().angularVelocity=new Vector3(0.0f,0.0f,0.0f);
            }
        }
    }
}
