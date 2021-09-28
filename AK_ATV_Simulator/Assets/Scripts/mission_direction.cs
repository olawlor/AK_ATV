using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mission_direction : MonoBehaviour
{
    public Transform playerTransform;
    Vector3 objPos;
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        objPos = Vector3.zero;
    }
    void objectiveStarted(Vector3 obj){
        objPos = obj;
    }
    void objectiveEnded(){
        objPos = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
         dir.x = playerTransform.eulerAngles.y - objPos.y;
        transform.localEulerAngles = dir;
    }
}
