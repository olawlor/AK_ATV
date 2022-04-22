/*! \file ControllerMobile.cs
 *  \brief The source for the class ControllerMobile
 */
// Based off existing code in ControllerKeyboard.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMobile : MonoBehaviour
{
    public VehicleProperties vehicle;

    public Button acceleration;
    public Button brake;
    public Button leftTurn;
    public Button rightTurn;
    public Button pause;

    // Debug values
    public float motor = 0.0f;
    public float rotate = 0.0f;
    // -------

    void Start()
    {
        vehicle = GetComponent<VehicleProperties>();
    }

    void FixedUpdate()
    {
        acceleration.onClick.AddListener(Acceleration);
        brake.onClick.AddListener(Brake);
        leftTurn.onClick.AddListener(LeftTurn);
        rightTurn.onClick.AddListener(RightTurn);
        vehicle.complementary_filter(1.5f * Time.fixedDeltaTime, ref vehicle.cur_motor_power, motor);
        vehicle.complementary_filter(1.5f * Time.fixedDeltaTime, ref vehicle.cur_steer, rotate);
    }

    void Acceleration()
    {
        motor = +3.0f;
    }

    void Brake()
    {
        motor = -3.0f;
    }

    void LeftTurn()
    {
        rotate = -1.0f;
    }

    void RightTurn()
    {
        rotate = 1.0f;
    }

    void Pause()
    {
        //Add Pause handler
    }

}
