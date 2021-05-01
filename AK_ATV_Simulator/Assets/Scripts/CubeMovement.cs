/* Scenario indicator pointer */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public GameObject baseObject;
    public float speed = 0.0f;
    public float height = 0.0f;

    private Vector3 pos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        transform.position = baseObject.transform.position;
        pos = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newpos=new Vector3(pos.x,pos.y,pos.z);
        newpos.y += Mathf.Abs(Mathf.Sin(Time.time * speed)) * height;
        transform.position = newpos;
    }
}

