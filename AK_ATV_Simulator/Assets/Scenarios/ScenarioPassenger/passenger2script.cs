using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passenger2script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 com = new Vector3(rb.centerOfMass.x, rb.centerOfMass.y, rb.centerOfMass.z - 3.0f);
        rb.centerOfMass = com;
    }
}
