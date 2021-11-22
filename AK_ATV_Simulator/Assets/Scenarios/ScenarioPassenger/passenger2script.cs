/*
 Artificially lower the passenger's center of mass, to make them less tippy.
*/
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passenger2script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float lowering=1.5f; // in meters
        Vector3 com = new Vector3(rb.centerOfMass.x, rb.centerOfMass.y-lowering, rb.centerOfMass.z);
        rb.centerOfMass = com;
    }
}
