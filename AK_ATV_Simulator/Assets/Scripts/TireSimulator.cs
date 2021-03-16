/**
 This does everything that we need with a tire:
    - Drive the motor
    - Steering (adjusts local angle)
    - Interface with suspension
*/
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireSimulator : MonoBehaviour
{
    /*! \ This is our parent vehicle GameObject
    */
    public GameObject parent;
    
    /*! \ This is a part of the parent
    */
    private VehicleProperties vehicle;

    /*! \ Physics objects
    */
    private Rigidbody rb;
    private Rigidbody vehicle_rb;
    private ConfigurableJoint suspension;
    
    public float steer=0.0f; /*! \ main Y rotation while steering
    */
    /*! \ Start is called before the first frame update
    */
    void Start()
    {
        vehicle = parent.GetComponent<VehicleProperties>();
        rb = GetComponent<Rigidbody>();
        vehicle_rb = parent.GetComponent<Rigidbody>();
        
        /*! \ Let wheels spin fast (normally capped at 7 rads/sec)
        */
        rb.maxAngularVelocity=vehicle.max_angular_velocity; /*! \ in radians/second */
        rb.mass=vehicle.mass_tire;
        rb.angularDrag=vehicle.angular_drag_tire;
        
        /*! \ Create the suspension joint that holds our wheel to our parent */
        suspension = gameObject.AddComponent<ConfigurableJoint>();
        suspension.connectedBody = transform.parent.gameObject.GetComponent<Rigidbody>();
        suspension.anchor = new Vector3(0.0f,0.0f,0.0f);
        suspension.autoConfigureConnectedAnchor = false;
        Vector3 p=transform.localPosition;
        suspension.connectedAnchor = new Vector3(p.x,p.y,p.z); /*! \ anchor point is our center point */
        suspension.swapBodies = true;
        suspension.xMotion = ConfigurableJointMotion.Locked; 
        suspension.yMotion = ConfigurableJointMotion.Limited; /*! \ wheel vertical suspension */
        suspension.zMotion = ConfigurableJointMotion.Locked;
        suspension.angularXMotion = ConfigurableJointMotion.Free; /*! \ wheel rotation */
        suspension.angularYMotion = ConfigurableJointMotion.Locked;
        suspension.angularZMotion = ConfigurableJointMotion.Locked; 
        
        SoftJointLimitSpring spr = new SoftJointLimitSpring();
        spr.spring = 8000.0f;
        spr.damper = 500.0f;
        suspension.linearLimitSpring = spr;
        
        SoftJointLimit soft = new SoftJointLimit();
        soft.bounciness = 0.0f;
        soft.limit = 0.01f;
        soft.contactDistance = 0.01f;
        suspension.linearLimit = soft;
        
    }

    /*! \ Update is called once per physics frame */
    void FixedUpdate()
    {
        if (suspension) {
            vehicle.draw_force(Color.red, transform.position, 
                suspension.currentForce);
        }
        
        /*! \ Drive any driving wheels */
        if (vehicle.drive_wheels>=4 || vehicle.cur_motor_power<0.0f || steer==0.0f) 
        {
            /*! \ Compute motor torque */
            Vector3 t=transform.right.normalized*vehicle.cur_motor_power*vehicle.max_motor_torque;
            /*! \ Apply torque to wheels */
            rb.AddTorque(t);
            /*! \ Apply reaction torque to vehicle body (for conservation of angular momentum) */
            vehicle_rb.AddTorque(-t);
        }
        
        /*! \ Rotate steering parts of suspension */
        if (steer!=0.0f) {
            if (!vehicle.is_VR) transform.parent.localRotation=Quaternion.Euler(0.0f,vehicle.cur_steer*steer,0.0f);
            else transform.parent.localRotation=Quaternion.Euler(Vector3.up * vehicle.cur_steer);
        }
    }
}
